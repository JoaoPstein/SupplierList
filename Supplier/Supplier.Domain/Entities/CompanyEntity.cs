using Supplier.Domain.Enum;

namespace Supplier.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public CompanyEntity(string fantasyName, string cnpj, UfEnum uf)
        {
            FantasyName = fantasyName;
            Cnpj = cnpj;
            Uf = uf;
        }

        public void Update(string fantasyName, string cnpj, UfEnum uf)
        {
            FantasyName = fantasyName;
            Cnpj = cnpj;
            Uf = uf;
        }

        public string FantasyName { get; protected set; }
        public string Cnpj { get; protected set; }
        public UfEnum Uf { get; protected set; }
    }
}
