using Amado.Common.Validators;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amado.MVC.Areas.Admin.Models.Product
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Ürün kodu zorunludur!")]
        [MaxLength(20, ErrorMessage = "Ürün kodu 20 karakterden fazla olamaz!")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Ürün fiyatı zorunludur!")]
        [Range(1,int.MaxValue,ErrorMessage ="Fiyat 0dan büyük olmalıdır!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur!")]
        [MaxLength(50, ErrorMessage = "Ürün kodu 50 karakterden fazla olamaz!")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Stok durumu hakkında bilgi zorunludur!")]
        public bool StockShortage { get; set; }

        [Required(ErrorMessage = "Ürün açıklaması zorunludur!")]
        [MaxLength(500, ErrorMessage = "Ürün açıklaması 500 karakterden fazla olamaz!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Birimin belirtilmesi zorunludur!")]
        [MaxLength(20, ErrorMessage = "Birim 20 karakterden fazla olamaz!")]
        public string UnitInfo { get; set; }
           
        [Required(ErrorMessage ="Kategori seçimi zorunludur!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Marka seçimi zorunludur!")]
        public int BrandId { get; set; }
        [Image(ErrorMessage = "En fazla 4 adet resim yükleyebilirsiniz. Yüklenecek resimler png/jpg/jpeg uzantılı olmalıdır!")]
        public List<IFormFile> Images { get; set; }
    }
}
