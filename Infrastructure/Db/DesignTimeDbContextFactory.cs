using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Db
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration to read from appsettings.json
            var baseDirectory = Directory.GetCurrentDirectory();
            var relativePath = Path.Combine(baseDirectory, "../SSO");

            // Use GetFullPath to resolve the relative path correctly
            var resolvedPath = Path.GetFullPath(relativePath);
            var path = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(resolvedPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = null
            };
            // Use the connection string from the configuration
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options, httpContextAccessor);
        }
    }
}
