


using Amado.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<List<ProductImage>> GetImagesByProductIdAsync(int id);
    }
}
