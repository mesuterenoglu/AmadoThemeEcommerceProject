

using Amado.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Product>> GetProductsbyCategoryIdAsync(int categoryId);
    }
}
