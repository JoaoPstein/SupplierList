using FluentAssertions;
using NSubstitute;
using Supplier.Application.DTOs.Companys;
using Supplier.Application.Services;
using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using Supplier.Domain.Interfaces;
using Supplier.Tests.Unit.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Supplier.Tests.Unit.Application.Services
{
    public class CompanyServiceTest
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyRepository _companyRepository;

        public CompanyServiceTest()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();

            _companyService = new CompanyService(_companyRepository);
        }

        [Fact]
        public void Should_Create_Company()
        {
            var company = CreateCompanyRequest(Guid.NewGuid());

            _companyService.Create(company);

            _companyRepository.Received(1).Create(Arg.Is<CompanyEntity>(x =>
                x.FantasyName == company.FantasyName
             && x.Cnpj == company.Cnpj
             && x.Uf == company.UF));
        }

        [Fact]
        public async Task Should_Delete_Company()
        {
            var companyId = Guid.NewGuid();
            var company = CreateBuilderCompany("Teste", companyId);

            _companyRepository.GetById(companyId).Returns(company);

            await _companyService.Delete(companyId);

            await _companyRepository.Received(1)
                .Delete(companyId);
        }

        [Fact]
        public async Task Should_GetAll_Company()
        {
            var companyIdA = Guid.NewGuid();
            var companyA = CreateBuilderCompany("Companhia A", companyIdA);

            var companyIdB = Guid.NewGuid();
            var companyB = CreateBuilderCompany("Companhia B", companyIdB);

            var companys = new List<CompanyEntity>();
            companys.Add(companyA);
            companys.Add(companyB);

            _companyRepository.GetAll().Returns(companys.AsQueryable());

            var companyReturns = await _companyService.GetAll();

            companyReturns.Should().HaveCount(2);

            companyReturns[0].FantasyName.Should().Be(companyA.FantasyName);
            companyReturns[0].Cnpj.Should().Be(companyA.Cnpj);
            companyReturns[0].UF.Should().Be(companyA.Uf);
            companyReturns[0].Active.Should().Be(companyA.Active);

            companyReturns[1].FantasyName.Should().Be(companyB.FantasyName);
            companyReturns[1].Cnpj.Should().Be(companyB.Cnpj);
            companyReturns[1].UF.Should().Be(companyB.Uf);
            companyReturns[1].Active.Should().Be(companyB.Active);
        }

        [Fact]
        public async Task Should_GetById_Company()
        {
            var companyId = Guid.NewGuid();
            var company = CreateBuilderCompany("Companhia A", companyId);

            _companyRepository.GetById(companyId).Returns(company);

            var companyReturns = await _companyService.GetById(companyId);

            companyReturns.FantasyName.Should().Be(company.FantasyName);
            companyReturns.Cnpj.Should().Be(company.Cnpj);
            companyReturns.UF.Should().Be(company.Uf);
            companyReturns.Active.Should().Be(company.Active);
        }

        [Fact]
        public async Task Should_Update_Company()
        {
            var companyId = Guid.NewGuid();
            var company = CreateBuilderCompany("Testando", companyId);

            var companyUpdate = CreateCompanyRequest(companyId);

            _companyRepository.GetById(company.Id).Returns(company);

            await _companyService.Update(company.Id, companyUpdate);

            await _companyRepository.Received(1).Update(company.Id, Arg.Is<CompanyEntity>(x =>
                                                                    x.Active == company.Active
                                                                 && x.FantasyName == company.FantasyName
                                                                 && x.Cnpj == company.Cnpj
                                                                 && x.Uf == company.Uf));
        }

        private CompanyRequestDto CreateCompanyRequest(Guid id)
        {
            return new CompanyRequestDto()
            {
                Active = true,
                Cnpj = "12345678912345",
                FantasyName = "Teste",
                UF = UfEnum.CE
            };
        }

        private CompanyEntity CreateBuilderCompany(string fantasyName, Guid id)
        {
            return new CompanyBuilders()
                .WithId(id)
                .WithFantasyName(fantasyName)
                .WithCnpj("12345678912345")
                .WithUf(UfEnum.AC)
                .Active()
                .Build();
        }
    }
}
