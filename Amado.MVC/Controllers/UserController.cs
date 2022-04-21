using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.MVC.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amado.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUserDto appUser = _mapper.Map<AppUserDto>(model);
                    appUser.Id = Guid.NewGuid().ToString();
                    var result = await _userService.RegisterAsync(appUser, model.Password);
                    if (result == Messages.Completed)
                    {
                        await _userService.LoginAsync(model.Email, model.Password, false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", result);
                    return View(model);

                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.LoginAsync(model.Email, model.Password, model.RememberMe);
                    if (result == Messages.Completed)
                    {
                        return Redirect(TempData["returnUrl"]== null ? "/home/index": TempData["returnUrl"].ToString());
                    }

                    ModelState.AddModelError("", result);
                    return View(model);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult NewPassword()
        {
            return View();
        }
    }
}
