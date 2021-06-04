using FluentAssertions;
using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using Supplier.Tests.Unit.Builders;
using Xunit;

namespace Supplier.Tests.Unit.Domain.Entities
{
    public class CompanyEntityTest
    {
        [Fact]
        public void Should_Create_Company()
        {
            var company = CreateCompany("Teste", "12345678910123", UfEnum.SC);

            var newCompany = new CompanyEntity(company.FantasyName, company.Cnpj, company.Uf);

            newCompany.FantasyName.Should().Be(company.FantasyName);
            newCompany.Cnpj.Should().Be(company.Cnpj);
            newCompany.Uf.Should().Be(company.Uf);
            newCompany.Active.Should().Be(company.Active);
        }

        [Fact]
        public void Should_Disable_Company()
        {
            var company = CreateCompany("Testando", "12345678910123", UfEnum.AC);

            company.Disable();

            company.Active.Should().BeFalse();
        }

        [Fact]
        public void Should_Update_Company()
        {
            var company = CreateCompany("Testando", "12345678910123", UfEnum.AL);

            company.Update("Teste", "09876543209876", UfEnum.GO);

            company.FantasyName.Should().Be("Teste");
            company.Cnpj.Should().Be("09876543209876");
            company.Uf.Should().Be(UfEnum.GO);
        }

        private CompanyEntity CreateCompany(string fantasyName, string cnpj, UfEnum ufEnum)
        {
            return new CompanyBuilders()
                .WithFantasyName(fantasyName)
                .WithCnpj(cnpj)
                .WithUf(ufEnum)
                .Active()
                .Build();
        }
    }
}
