

using System.Collections.Generic;

namespace Amado.Core.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<ProductDto> Products { get; set; }
        public bool IsActive { get; set; }
    }
}
