using EFCore_LazyLoading_LINQ.Data.Configurations;
using EFCore_LazyLoading_LINQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_LazyLoading_LINQ.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Office> Offices { get; set; } = null!;
        public DbSet<Title> Titles { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<EmployeeProject> EmployeeProjects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new TitleConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
        }
    }
}
