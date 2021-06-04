using Supplier.Application.DTOs.Companys;
using Supplier.Domain.Entities;
using Supplier.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void Create(CompanyRequestDto requestDto)
        {
            var company = new CompanyEntity(requestDto.FantasyName, requestDto.Cnpj, requestDto.UF);
            _companyRepository.Create(company);
        }

        public async Task Delete(Guid id)
        {
            var companyById = await _companyRepository.GetById(id);
            companyById.Disable();
            _companyRepository.Delete(companyById);
        }

        public async Task<IList<CompanyResponseDto>> GetAll()
        {
            var getAll = await _companyRepository.GetAll();

            return getAll.Select(x => new CompanyResponseDto()
            {
                Active = x.Active,
                UF = x.Uf,
                Cnpj = x.Cnpj,
                FantasyName = x.FantasyName
            }).ToList();
        }

        public async Task<CompanyResponseDto> GetById(Guid id)
        {
            var company = await _companyRepository.GetById(id);
            return new CompanyResponseDto()
            {
                Active = company.Active,
                Cnpj = company.Cnpj,
                FantasyName = company.FantasyName,
                UF = company.Uf,
            };
        }

        public async Task Update(Guid id, CompanyRequestDto requestDto)
        {
            var company = await _companyRepository.GetById(id);

            company.Update(requestDto.FantasyName, requestDto.Cnpj, requestDto.UF);

            _companyRepository.Update(id, company);
        }
    }
}
