using Supplier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplier.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IList<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
