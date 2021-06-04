using Supplier.Domain.Entities;
using Supplier.Domain.Interfaces;
using Supplier.Infra.Context;
using System;
using System.Collections.Generic;
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


        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
