using Microsoft.EntityFrameworkCore;

namespace Piles.DbContexts
{
    public class PilesDbContextFactory : IPilesDbContextFactory
    {
        private readonly string _connectionString;

        public PilesDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PilesDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new PilesDbContext(options);
        }
    }
}
