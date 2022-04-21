


using Amado.Core.Entities;
using Amado.Core.FilterModel;
using Amado.DataAccess.Context;
using Amado.DataAccess.Repositories.Base;
using Amado.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories
{
    public class ProductRepository :BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> GetProductsbyCategoryIdAsync(int categoryId)
        {
            var products = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            return products;
        }

        public List<Product> GetProductsbyFilterAsync(FilterViewModel model)
        {
            List<Product> products = new List<Product>();

            if (model.Categories!= null && model.Categories.Length >0)
            {
                foreach (var category in model.Categories)
                {
                    var cp = _context.Products.Where(x => x.CategoryId == category);
                    products.AddRange(cp);
                }
            }
            if (model.Brands != null && model.Brands.Length >0)
            {
                foreach (var brand in model.Brands)
                {
                    var bp = _context.Products.Where(x => x.BrandId == brand);
                    foreach (var item in bp)
                    {
                        if (!products.Contains(item))
                        {
                            products.Add(item);
                        }
                    }
                }
            }

            if (model.SortingOption != null)
            {
                switch (model.SortingOption)
                {
                    case "asc":
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case "desc":
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            return products;

        }
    }
}
