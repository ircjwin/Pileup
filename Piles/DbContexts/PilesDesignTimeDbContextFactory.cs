using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Piles.DbContexts
{
    internal class PilesDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PilesDbContext>
    {
        public PilesDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=Piles.db").Options;

            return new PilesDbContext(options);
        }
    }
}
