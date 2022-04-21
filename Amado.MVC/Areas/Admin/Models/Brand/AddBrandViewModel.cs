

using System.ComponentModel.DataAnnotations;

namespace Amado.MVC.Areas.Admin.Models.Brand
{
    public class AddBrandViewModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur!")]
        [MaxLength(50, ErrorMessage = "Kategori adı 50 karakterden fazla olamaz!")]
        public string BrandName { get; set; }
    }
}
