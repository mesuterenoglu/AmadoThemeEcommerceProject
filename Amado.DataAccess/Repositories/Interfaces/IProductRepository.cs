
using Amado.Core.DTOs;
using Amado.Core.Entities;
using Amado.Core.FilterModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsbyCategoryIdAsync(int categoryId);
        List<Product> GetProductsbyFilterAsync(FilterViewModel model);
    }
}
