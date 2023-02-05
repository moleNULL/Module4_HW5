using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFCore_LazyLoading_LINQ.Entities
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        // Create context configuration for EF Core during Migrations
        public ApplicationContext CreateDbContext(string[] args)
        {
            string jsonSettingsFile = "appsettings.json";

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(jsonSettingsFile);

            var config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");

            if (connectionString is null)
            {
                throw new Exception("connectionString is null");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            return new ApplicationContext(options);
        }
    }
}
