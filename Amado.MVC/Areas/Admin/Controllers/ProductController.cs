using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.MVC.Areas.Admin.Models.Product;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ICategoryService categoryService,IBrandService brandService, IProductImageService productImageService, IProductService productService,IMapper mapper)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllActive()
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

        public async Task<IActionResult> GetAllInActive()
        {
            try
            {
                var products = await _productService.GetAllInActiveAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<ProductDto>(model);
                    product.ProductImages = new List<ProductImageDto>();
                    if (model.Images.Count > 0)
                    {
                        foreach (var file in model.Images)
                        {
                            var fileName = await _productImageService.AddImageServer(file);
                            ProductImageDto productImageDto = new ProductImageDto
                            {
                                IsActive = true,
                                Url = fileName
                            };
                            product.ProductImages.Add(productImageDto);
                        }
                    }
                    else
                    {
                        product.ProductImages.Add(new ProductImageDto
                        {
                            IsActive = true,
                            Url = "/img/product-img/no-image.jpg"
                        });
                    }
                    var result = await _productService.AddAsync(product);
                    if (result == Messages.Added)
                    {
                        return Redirect("/admin/product/getallactive");
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

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                {
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }
                var product = await _productService.GetbyIdAsync(Convert.ToInt32(id));
                if (product == null)
                {
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }
                var model = _mapper.Map<UpdateProductViewModel>(product);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _productService.GetbyIdAsync(id);
                    product.Price = model.Price;
                    product.ProductName = model.ProductName;
                    product.StockShortage = model.StockShortage;
                    product.Description = model.Description;
                    product.UnitInfo = model.UnitInfo;
                    product.CategoryId = model.CategoryId;
                    product.BrandId = model.BrandId;
                    product.IsActive = model.IsActive;
                    var result = await _productService.UpdateAsync(product);
                    if (result == Messages.Updated)
                    {
                        return Redirect("/Admin/Product/GetAllActive");
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
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                {
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }

                var resultProduct = await _productService.AnyAsync(x => x.Id == id);
                if (!resultProduct)
                {
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }

                var result = await _productService.DeleteAsync(Convert.ToInt32(id));
                if (result == Messages.Deleted)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Product/GetAllActive");
                }

                ViewBag.Error = Messages.MissingProductId;
                return Redirect("/Admin/Product/GetAllActive");

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
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }

                var resultProduct = await _productService.AnyAsync(x => x.Id == id);
                if (!resultProduct)
                {
                    ViewBag.Error = Messages.MissingProductId;
                    return Redirect("/Admin/Product/GetAllActive");
                }
                
                var result = await _productService.RemoveFromDbAsync(Convert.ToInt32(id));
                if (result == Messages.Deleted)
                {
                    ViewBag.Error = null;
                    return Redirect("/Admin/Product/GetAllActive");
                }

                ViewBag.Error = Messages.MissingProductId;
                return Redirect("/Admin/Product/GetAllActive");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
