using Supplier.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(Guid id);
        Task Create(T entity);
        Task Update(Guid id, T entity);
        Task Delete(Guid id);
    }
}
