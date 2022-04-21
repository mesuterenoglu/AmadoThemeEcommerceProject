
using Amado.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<List<Product>> GetProductsbyBrandIdAsync(int brandId);
    }
}
