using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.FilterModel;
using Amado.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Amado.MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService,ICategoryService categoryService,IBrandService brandService)
        {
           _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllActiveAsync();
                return View(products);
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
                    var product = await _productService.GetbyIdAsync(Convert.ToInt32(id));
                    return View(product);
                }
                ViewBag.Error = Messages.MissingProductId;
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public IActionResult GetProductbyFilter(FilterViewModel model)
        {
            var context = HttpContext.Request.Body;
            try
            {
                var products = _productService.GetProductsbyFilterAsync(model);
                return Json(products);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
