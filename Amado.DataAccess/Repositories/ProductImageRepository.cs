
using Amado.Core.Entities;
using Amado.DataAccess.Context;
using Amado.DataAccess.Repositories.Base;
using Amado.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories
{
    public class ProductImageRepository: BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<ProductImage>> GetImagesByProductIdAsync(int id)
        {
            var images = await _context.ProductImages.Where(x => x.ProductId == id).ToListAsync();
            return images;
        }
    }
}
