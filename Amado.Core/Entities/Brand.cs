

using Amado.Core.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amado.Core.Entities
{
    public class Brand : BaseEntity
    {
        [Required(ErrorMessage ="Marka adı zorunludur!")]
        [MaxLength(50, ErrorMessage ="Marka adı 50 karakterden fazla olamaz!")]
        public string BrandName { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
