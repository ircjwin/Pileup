using Piles.Models;
using Microsoft.EntityFrameworkCore;

namespace Piles.DbContexts
{
    public class PilesDbContext : DbContext
    {
        public PilesDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        public DbSet<PileDb> Piles { get; set; }

        public DbSet<RuminationDb> Ruminations { get; set; }
    }
}
