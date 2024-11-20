using Supplier.Domain.Entities;
using Supplier.Domain.Interfaces;
using Supplier.Infra.Context;

namespace Supplier.Infra.Repository
{
    public class CompanyRepository : GenericRepository<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(MainContext dbContext) : base(dbContext)
        {
        }
    }
}
