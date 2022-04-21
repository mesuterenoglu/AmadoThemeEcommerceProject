using Amado.Core.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amado.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Kategori adı zorunludur!")]
        [MaxLength(50, ErrorMessage = "Kategori adı 50 karakterden fazla olamaz!")]
        public string CategoryName { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
