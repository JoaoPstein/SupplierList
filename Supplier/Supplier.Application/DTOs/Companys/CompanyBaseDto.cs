using Supplier.Domain.Enum;

namespace Supplier.Application.DTOs.Companys
{
    public abstract class CompanyBaseDto : BaseDto
    {
        public string FantasyName { get; set; }
        public UfEnum UF { get; set; }
        public string Cnpj { get; set; }
    }
}
