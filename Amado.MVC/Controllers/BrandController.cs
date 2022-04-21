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
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        
        public async Task<IActionResult> Index()
        {
            try
            {
                var brands = await _brandService.GetAllActiveAsync();
                var filledList = brands.Where(x => x.Products.Count > 0).OrderBy(x => x.BrandName).ToList();
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
                if (id != null && id > 0)
                {
                    var products = await _brandService.GetProductsbyBrandIdAsync(Convert.ToInt32(id));
                    return View(products);
                }
                ViewBag.Error = Messages.MissingBrandId;
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
