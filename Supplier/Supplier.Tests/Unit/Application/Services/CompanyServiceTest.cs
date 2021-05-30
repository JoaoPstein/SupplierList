using NSubstitute;
using Supplier.Application.DTOs.Companys;
using Supplier.Application.Services;
using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using Supplier.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Supplier.Tests.Unit.Application.Services
{
    public class CompanyServiceTest
    {
        private ICompanyService _companyService;
        private ICompanyRepository _companyRepository;

        public CompanyServiceTest()
        {
            _companyService = Substitute.For<ICompanyService>();
            _companyRepository = Substitute.For<ICompanyRepository>();
        }

        [Fact]
        public void CreateCompany()
        {
            var company = CreateCompantRequest();

            _companyService.Create(company);

            _companyRepository.Received(1).Create(Arg.Is<CompanyEntity>(x =>
                x.FantasyName == company.FantasyName
             && x.Cnpj == company.Cnpj
             && x.Uf == company.UF));
        }

        private CompanyRequestDto CreateCompantRequest()
        {
            return new CompanyRequestDto()
            {
                Active = true,
                Cnpj = "12345678912345",
                FantasyName = "Teste",
                UF = UfEnum.CE
            };
        }
    }
}
