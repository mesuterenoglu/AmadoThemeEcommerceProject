

using System.Collections.Generic;

namespace Amado.Core.DTOs
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public List<ProductDto> Products { get; set; }
        public bool IsActive { get; set; }
    }
}
