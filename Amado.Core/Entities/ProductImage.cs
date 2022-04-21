
using Amado.Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Core.Entities
{
    public class ProductImage : BaseEntity
    {
        [Required(ErrorMessage = "Ürün fotoğraf linki zorunludur!")]
        [MaxLength(500, ErrorMessage = "Ürün fotoğraf linki uzunluğu 500 karakterden fazla olamaz!")]
        public string Url { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
