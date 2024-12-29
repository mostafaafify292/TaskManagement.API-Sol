using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repository.Contract;
using TaskManagement.Repository.Data;

namespace TaskManagement.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TaskManagementDbContext _dbContext;

        public GenericRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
             await _dbContext.Set<T>().AddAsync(entity);
             await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (filter != null)
            {
                 query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
