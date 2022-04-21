

using System.Collections.Generic;

namespace Amado.Core.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public bool StockShortage { get; set; }
        public string Description { get; set; }
        public string UnitInfo { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public int BrandId { get; set; }
        public BrandDto Brand { get; set; }
        public List<ProductImageDto> ProductImages { get; set; }
        public bool IsActive { get; set; }
    }
}
