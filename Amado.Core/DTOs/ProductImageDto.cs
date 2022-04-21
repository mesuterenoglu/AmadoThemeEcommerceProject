

namespace Amado.Core.DTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public bool IsActive { get; set; }
    }
}
