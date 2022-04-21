using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.MVC.Areas.Admin.Models.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amado.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var brands = await _brandService.GetAllActiveAsync();
                return View(brands);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IActionResult> GetAllInActive()
        {
            try
            {
                var brands = await _brandService.GetAllInActiveAsync();
                return View(brands);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBrandViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var resultBrand = await _brandService.AnyAsync(x => x.BrandName == model.BrandName);
                    if (resultBrand)
                    {
                        ModelState.AddModelError("", Messages.DuplicateBrand);
                        return View(model);
                    }

                    var result = await _brandService.AddAsync(new BrandDto { BrandName = model.BrandName });
                    if (result == Messages.Added)
                    {
                        return Redirect("/Admin/Brand/GetAllActive");
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
        public async Task<IActionResult> Update(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }
                var brand = await _brandService.GetbyIdAsync(Convert.ToInt32(id));
                if (brand == null)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }
                return View(brand);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandDto brandDto)
        {
            try
            {
                var resultUpdate = await _brandService.UpdateAsync(brandDto);
                if (resultUpdate == Messages.Updated)
                {
                    return Redirect("/Admin/Brand/GetAllActive");
                }
                ViewBag.Error = resultUpdate;
                return Redirect("/Admin/Brand/GetAllActive");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                var resultBrand = await _brandService.AnyAsync(x => x.Id == id);
                if (!resultBrand)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                var result = await _brandService.DeleteAsync(Convert.ToInt32(id));
                if (result == Messages.Deleted)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                ViewBag.Error = Messages.MissingBrandId;
                return Redirect("/Admin/Brand/GetAllActive");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromDb(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                var resultBrand = await _brandService.AnyAsync(x => x.Id == id);
                if (!resultBrand)
                {
                    ViewBag.Error = Messages.MissingBrandId;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                var result = await _brandService.RemoveFromDbAsync(Convert.ToInt32(id));
                if (result == Messages.RemovedFromDB)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Brand/GetAllActive");
                }

                ViewBag.Error = Messages.MissingBrandId;
                return Redirect("/Admin/Brand/GetAllActive");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
