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
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> GetProductsbyBrandIdAsync(int brandId)
        {
            var products = await _context.Products.Where(x => x.BrandId == brandId).ToListAsync();
            return products;
        }
    }
}
