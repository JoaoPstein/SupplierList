using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supplier.Domain.Entities;

namespace Supplier.Infra.Mapping
{
    public class CompanyMapping : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.Property(x => x.FantasyName).IsRequired();
            builder.Property(x => x.Uf).IsRequired();
            builder.Property(x => x.Cnpj).IsRequired();
            builder.Property(x => x.Active).IsRequired();
        }
    }
}
