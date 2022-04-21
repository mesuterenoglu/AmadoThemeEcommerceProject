using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Amado.MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
           _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllActiveAsync();
                var filledList = categories.Where(x => x.Products.Count > 0).OrderBy(x=>x.CategoryName).ToList();
                return View(filledList);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id != null && id>0)
                {
                    var products = await _categoryService.GetProductsbyCategoryIdAsync(Convert.ToInt32(id));
                    return View(products);
                }
                ViewBag.Error = Messages.MissingCategoryId;
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
