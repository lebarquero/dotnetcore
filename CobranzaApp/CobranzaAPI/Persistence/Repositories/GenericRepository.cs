using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CobranzaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CobranzaAPI.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CobranzaContext _dbContext;

        public GenericRepository(CobranzaContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        
        public async Task<IList<T>> ListAsync(Expression<Func<T, bool>> filter = null, bool asNoTracking = true)
        {
            IQueryable<T> qry = _dbContext.Set<T>();

            if (filter != null)
            {
                qry = qry.Where(filter);
            }

            if (!asNoTracking)
            {
                return await qry.ToListAsync();
            }

            return await qry.AsNoTracking().ToListAsync();
        }
        
        public async Task<bool> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return await SaveAsync();
        }
        
        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await SaveAsync();
        }
        
        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}