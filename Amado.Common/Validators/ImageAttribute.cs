using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amado.Common.Validators
{
    public class ImageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var files = value as List<IFormFile>;

                var result = false;

                if (files.Count > 4)
                {
                    return result;
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
                        return result;
                    }
                }
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
