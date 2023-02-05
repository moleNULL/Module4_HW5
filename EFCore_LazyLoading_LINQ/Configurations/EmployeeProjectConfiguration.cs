using EFCore_LazyLoading_LINQ.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_LazyLoading_LINQ.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            // TABLE NAME
            builder.ToTable("EmployeeProject");

            // PRIMARY KEY
            builder.HasKey(ep => ep.EmployeeProjectId);

            // CONSTRAINTS
            builder.Property(ep => ep.Rate).HasColumnType("money");
        }
    }
}
