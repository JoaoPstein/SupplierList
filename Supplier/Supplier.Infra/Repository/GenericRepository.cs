using Microsoft.EntityFrameworkCore;
using Supplier.Domain.Entities;
using Supplier.Domain.Interfaces;
using Supplier.Infra.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Infra.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly MainContext _dbContext;

        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>()
              .AsNoTracking()
              .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Guid id, T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
