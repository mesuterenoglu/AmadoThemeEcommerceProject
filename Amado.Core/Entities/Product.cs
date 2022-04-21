

using Amado.Core.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Core.Entities
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage ="Ürün kodu zorunludur!")]
        [MaxLength(20,ErrorMessage ="Ürün kodu 20 karakterden fazla olamaz!")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Ürün fiyatı zorunludur!")]
        [Range(1, int.MaxValue, ErrorMessage = "Fiyat 0dan büyük olmalıdır!")]
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

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        public virtual List<ProductImage> ProductImages { get; set; }

    }
}
