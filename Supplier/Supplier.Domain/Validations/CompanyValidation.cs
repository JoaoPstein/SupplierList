using FluentValidation;
using Supplier.Domain.Entities;

namespace Supplier.Domain.Validations
{
    public class CompanyValidation : AbstractValidator<CompanyEntity>
    {
        public CompanyValidation()
        {
            ValidateFantasyName();
            ValidateCnpj();
            ValidateUf();
        }

        private void ValidateFantasyName()
        {
            RuleFor(x => x.FantasyName)
               .NotEmpty()
               .WithMessage("Campo nome fantasia não pode ser nullo.");
        }

        public void ValidateCnpj()
        {
            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage("Campo Cnpj não pode ser vazio.")
                .MinimumLength(14)
                .WithMessage("Digite no minimo 14 caracteres.");
        }

        public void ValidateUf()
        {
            RuleFor(x => x.Uf)
                .NotEmpty()
                .WithMessage("Campo UF não pode ser vazio.");
        }
    }
}
