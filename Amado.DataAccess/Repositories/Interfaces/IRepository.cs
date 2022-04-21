
using Amado.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task RemoveFromDbAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllInActiveAsync();
        Task<List<TEntity>> GetAllActiveAsync();
        Task<TEntity> GetbyIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetbyFilterAsync(Expression<Func<TEntity, bool>> filter);
    }
}
