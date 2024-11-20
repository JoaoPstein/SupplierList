using Supplier.Domain.Entities;
using Supplier.Domain.Enum;
using System;

namespace Supplier.Tests.Unit.Builders
{
    public class CompanyBuilders
    {
        private string _fantasyName;
        private string _cnpj;
        private UfEnum _enumUF;
        private bool _active;
        private Guid _id;

        public CompanyEntity Build()
        {
            return new CompanyEntity(_fantasyName, _cnpj, _enumUF)
            {
                Id = _id
            };
        }

        public CompanyBuilders WithFantasyName(string fantasyName)
        {
            _fantasyName = fantasyName;
            return this;
        }

        public CompanyBuilders WithCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public CompanyBuilders WithUf(UfEnum ufEnum)
        {
            _enumUF = ufEnum;
            return this;
        }

        public CompanyBuilders Active()
        {
            _active = true;
            return this;
        }

        public CompanyBuilders WithId(Guid id )
        {
            _id = id;
            return this;
        }
    }
}
