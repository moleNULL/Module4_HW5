using EFCore_LazyLoading_LINQ.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_LazyLoading_LINQ.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // TABLE NAME
            builder.ToTable("Client");

            // PRIMARY KEY
            builder.HasKey(c => c.ClientId);

            // NOT NULL
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
        }
    }
}
