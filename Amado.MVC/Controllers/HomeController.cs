using Amado.BLL.Services.Interfaces;
using Amado.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Amado.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger,IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllActiveAsync();
                return View(products.Take(10).OrderBy(x=>x.ProductName).ToList());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public IActionResult Header()
        {
            return PartialView();
        }

        public IActionResult NewsletterArea()
        {
            return PartialView();
        }

        public IActionResult Footer()
        {
            return PartialView();
        }

        public IActionResult SearchWrapper()
        {
            return PartialView();
        }

        public IActionResult MobileNav()
        {
            return PartialView();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
