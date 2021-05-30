using Supplier.Application.DTOs.Companys;
using Supplier.Domain.Entities;
using Supplier.Domain.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
            //var company = new CompanyEntity(requestDto.FantasyNaame, requestDto.Cnpj, requestDto.UF);
            //_companyRepository.Create(company);
        }

        public async Task Delete(Guid id)
        {
            //var companyById = await _companyRepository.GetById(id);

            //if (companyById == null)
            //{
            //    throw new Exception("Companhia não encontrada.");
            //}

            //companyById.Disable();
            throw new NotImplementedException();
        }

        public Task<IList<CompanyResponseDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CompanyResponseDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, CompanyRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
