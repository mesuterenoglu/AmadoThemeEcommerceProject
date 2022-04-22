using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetImagesByProduct(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var images = await _productImageService.GetImagesByProductIdAsync(Convert.ToInt32(id));
                    return View(images);
                }
                ViewBag.Error = Messages.MissingProductId;
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IActionResult> RemoveFromDb(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var resultImage = await _productImageService.AnyAsync(x => x.Id == id);
                    if (resultImage)
                    {
                        var result = await _productImageService.RemoveFromDbAsync(Convert.ToInt32(id));
                        if (result == Messages.RemovedFromDB)
                        {
                            return Redirect("/admin/product/GetAllActive");
                        }
                    }
                }
                ViewBag.Error = Messages.MissingImageId;
                return Redirect("/admin/product/GetAllActive");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddNewImagebyProductId(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var images = await _productImageService.GetImagesByProductIdAsync(Convert.ToInt32(id));
                    return View(images);
                }
                ViewBag.Error = Messages.MissingProductId;
                return View();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewImagebyProductId(int id,List<IFormFile> files)
        {
            try
            {
                var images = await _productImageService.GetImagesByProductIdAsync(Convert.ToInt32(id));

                var count = images.Count + files.Count;
                if (count > 4)
                {
                    ModelState.AddModelError("", Messages.TooMuchImage);
                    return View(images);
                }
                List<string> validExtensions = new List<string>() {
                "png", "jpg", "jpeg"
                };
                foreach (var file in files)
                {
                    var fileExtension = file.FileName.Split(".");
                    var isValid = validExtensions.Contains(fileExtension[fileExtension.Length - 1]);
                    if (!isValid)
                    {
                        ModelState.AddModelError("", Messages.InvalidFileType);
                        return View(images);
                    }
                }

                for (int i = 0; i < files.Count; i++)
                {
                    var fileName = await _productImageService.AddImageServer(files[i]);
                    await _productImageService.AddAsync(new ProductImageDto
                    {
                        ProductId = id,
                        Url = fileName
                    });
                }
                return Redirect("/admin/product/GetAllActive");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
