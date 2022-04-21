using System.ComponentModel.DataAnnotations;

namespace Amado.MVC.Areas.Admin.Models.Category
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur!")]
        [MaxLength(50, ErrorMessage = "Kategori adı 50 karakterden fazla olamaz!")]
        public string CategoryName { get; set; }
    }
}
