using Microsoft.EntityFrameworkCore;
using Supplier.Application.DTOs.Companys;
using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using System.Threading.Tasks;

namespace Supplier.Integration.Test.Integration.CompanyTests
{
    public class CompanySetup
    {
        private static CompanySetup _companySetup;
        public CompanyEntity CompanyEntity { get; private set; }

        private CompanySetup(DbContext dbContext)
        {
            Task.FromResult(Seed(dbContext));
        }

        public static CompanySetup GetSetup(DbContext dbContext)
        {
            if (_companySetup == null)
            {
                _companySetup = new CompanySetup(dbContext);
            }
            return _companySetup;
        }

        public async Task Seed(DbContext _context)
        {
            string suffix = "crcm";
            var company = await _context.AddAsync(CreateCompany(suffix, UfEnum.SC));
            CompanyEntity = company.Entity;

            await _context.SaveChangesAsync();
        }

        public CompanyEntity CreateCompany(string suffix, UfEnum ufEnum)
        {
            return new CompanyEntity($"Company{suffix}", $"CNPJ{suffix}", ufEnum);
        }

        public CompanyResponseDto GetCompany()
        {
            return new CompanyResponseDto()
            {
                Active = CompanyEntity.Active,
                Cnpj = CompanyEntity.Cnpj,
                FantasyName = CompanyEntity.FantasyName,
                UF = CompanyEntity.Uf
            };
        }
    }
}
