using Amado.Core.Entities.Abstract;
using Amado.DataAccess.Context;
using Amado.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amado.DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> dataSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            dataSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dataSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dataSet.AnyAsync(filter);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetbyIdAsync(id);
            entity.IsActive = false;
            await UpdateAsync(entity);
        }

        public async Task<List<TEntity>> GetAllActiveAsync()
        {
            var list = await dataSet.Where(x => x.IsActive == true).ToListAsync();
            return list;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var list = await dataSet.ToListAsync();
            return list;
        }

        public async Task<List<TEntity>> GetAllInActiveAsync()
        {
            var list = await dataSet.Where(x => x.IsActive == false).ToListAsync();
            return list;
        }

        public async Task<List<TEntity>> GetbyFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            var list = await dataSet.Where(filter).ToListAsync();
            return list;
        }

        public async Task<TEntity> GetbyIdAsync(int id)
        {
            var entity = await dataSet.SingleOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task RemoveFromDbAsync(int id)
        {
            var entity = await dataSet.SingleOrDefaultAsync(x => x.Id == id);
            dataSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dataSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
