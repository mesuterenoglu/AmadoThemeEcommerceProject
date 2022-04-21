

using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amado.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<bool> AnybyEmailAsync(string email)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnybyIdAsync(string id)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id.ToString());
                if (appUser == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    throw new InvalidOperationException(Messages.MissingUserEmail);
                }
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public List<AppUserDto> GetAllActive()
        {
            try
            {
                var users = _userManager.Users.Where(x => x.IsActive == true).ToList();
                var list = _mapper.Map<List<AppUserDto>>(users);
                return list;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AppUserDto> GetbyEmailAsync(string email)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    throw new InvalidOperationException(Messages.MissingUserEmail);
                }
                var appUserDto = _mapper.Map<AppUserDto>(appUser);

                return appUserDto;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AppUserDto> GetbyIdAsync(string id)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id);
                if (appUser == null)
                {
                    throw new InvalidOperationException(Messages.MissingUserId);
                }
                var appUserDto = _mapper.Map<AppUserDto>(appUser);

                return appUserDto;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public bool IsSignIn(ClaimsPrincipal principal)
        {
            try
            {
                var result = _signInManager.IsSignedIn(principal);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> LoginAsync(string userName, string password, bool isPersistent)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, true);
                if (result.Succeeded)
                {
                    return Messages.Completed;
                }
                return Messages.InvalidPassword;
            }
            catch (Exception ex)
            {
               throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
               throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> NewEmailAsync(string email, string password, string newEmail)
        {
            try
            {
                var resultCheck = await CheckPasswordAsync(email, password);
                if (resultCheck)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    user.Email = newEmail;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Messages.Completed;
                    }
                    return Messages.Failed;
                }
                return Messages.InvalidPassword;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> NewPasswordAsync(string email, string password, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, password, newPassword);

                    if (result.Succeeded)
                    {
                        return Messages.Completed;
                    }

                    return Messages.Failed;
                }

                return Messages.MissingUserEmail;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> RegisterAsync(AppUserDto entity, string password)
        {
            try
            {
                var resultEmail = await AnybyEmailAsync(entity.Email);

                if (resultEmail)
                {
                    return Messages.DuplicateUserEmail;
                }

                var appUser = _mapper.Map<AppUser>(entity);

                appUser.UserName = entity.Email;
                
                var result = await _userManager.CreateAsync(appUser, password);

                if (result.Succeeded)
                {
                    return Messages.Completed;
                }
                return Messages.Failed;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(AppUserDto entity)
        {
            try
            {
                var user = _mapper.Map<AppUser>(entity);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Messages.Completed;
                }
                return Messages.Failed;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
