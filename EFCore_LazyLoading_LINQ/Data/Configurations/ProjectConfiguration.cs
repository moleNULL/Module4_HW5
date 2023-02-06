using EFCore_LazyLoading_LINQ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_LazyLoading_LINQ.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // TABLE NAME
            builder.ToTable("Project");

            // PRIMARY KEY
            builder.HasKey(p => p.ProjectId);

            // NOT NULL
            builder.Property(p => p.Name).IsRequired();

            // CONSTRAINTS
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Budget).HasColumnType("money");

            // FOREIGN KEYS and relationships

            // many to many (EmployeeProject)
            builder.HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<EmployeeProject>(
                    j => j
                        .HasOne(ep => ep.Employee)
                        .WithMany(e => e.EmployeeProjects)
                        .HasForeignKey(ep => ep.EmployeeId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                    .HasOne(ep => ep.Project)
                    .WithMany(p => p.EmployeeProjects)
                    .HasForeignKey(ep => ep.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
