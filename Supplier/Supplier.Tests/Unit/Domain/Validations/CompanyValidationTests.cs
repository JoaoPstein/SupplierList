using FluentValidation.TestHelper;
using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using Supplier.Domain.Validations;
using Xunit;

namespace Supplier.Tests.Unit.Domain.Validations
{
    public class CompanyValidationTests
    {
        private readonly CompanyValidation _companyValidation;

        public CompanyValidationTests()
        {
            _companyValidation = new CompanyValidation();
        }

        [Fact]
        private void Should_Return_Error_When_FantasyName_Is_Null()
        {
            var company = CreateCompanyEntity("", "12345678912345", UfEnum.SC);

            _companyValidation.ShouldHaveValidationErrorFor(x => x.FantasyName, company)
                    .WithErrorMessage("Campo nome fantasia não pode ser nullo.");
        }

        [Fact]
        private void Should_Return_Error_When_Cnpj_Is_Null()
        {
            var company = CreateCompanyEntity("Teste", "", UfEnum.SC);

            _companyValidation.ShouldHaveValidationErrorFor(x => x.FantasyName, company)
                    .WithErrorMessage("Campo Cnpj não pode ser vazio.");
        }

        [Fact]
        private void Should_Return_Error_When_Cnpj_Is_Invalid()
        {
            var company = CreateCompanyEntity("Teste", "12", UfEnum.SC);

            _companyValidation.ShouldHaveValidationErrorFor(x => x.FantasyName, company)
                    .WithErrorMessage("Digite no minimo 14 caracteres.");
        }

        private static CompanyEntity CreateCompanyEntity(string fantasyName, string cnpj, UfEnum ufEnum)
        {
            return new CompanyEntity(fantasyName, cnpj, ufEnum);
        }
    }
}
