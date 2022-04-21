using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.MVC.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amado.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var categories = await _categoryService.GetAllActiveAsync();
                return View(categories);
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
                var categories = await _categoryService.GetAllInActiveAsync();
                return View(categories);
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
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var resultCategory = await _categoryService.AnyAsync(x => x.CategoryName == model.CategoryName);
                    if (resultCategory)
                    {
                        ModelState.AddModelError("", Messages.DuplicateCategory);
                        return View(model);
                    }

                    var result = await _categoryService.AddAsync(new CategoryDto { CategoryName = model.CategoryName });
                    if (result == Messages.Added)
                    {
                        return Redirect("/Admin/Category/GetAllActive");
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
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }
                var category = await _categoryService.GetbyIdAsync(Convert.ToInt32(id));
                if (category == null)
                {
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }
                return View(category);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            try
            {
                var resultUpdate = await _categoryService.UpdateAsync(categoryDto);
                if (resultUpdate == Messages.Updated)
                {
                    return Redirect("/Admin/Category/GetAllActive");
                }
                ViewBag.Error = resultUpdate;
                return Redirect("/Admin/Category/GetAllActive");
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
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                var resultCategory = await _categoryService.AnyAsync(x => x.Id == id);
                if (!resultCategory)
                {
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                var result = await _categoryService.DeleteAsync(Convert.ToInt32(id));
                if (result == Messages.Deleted)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                ViewBag.Error = Messages.MissingCategoryId;
                return Redirect("/Admin/Category/GetAllActive");

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
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                var resultCategory = await _categoryService.AnyAsync(x => x.Id == id);
                if (!resultCategory)
                {
                    ViewBag.Error = Messages.MissingCategoryId;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                var result = await _categoryService.RemoveFromDbAsync(Convert.ToInt32(id));
                if (result == Messages.RemovedFromDB)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Category/GetAllActive");
                }

                ViewBag.Error = Messages.MissingCategoryId;
                return Redirect("/Admin/Category/GetAllActive");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
