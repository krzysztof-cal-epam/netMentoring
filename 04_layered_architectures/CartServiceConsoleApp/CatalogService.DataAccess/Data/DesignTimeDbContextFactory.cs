using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CatalogServiceDb;Trusted_Connection=True;");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
