using Microsoft.EntityFrameworkCore;

namespace Piles.Data
{
    public class PilesDbContext : DbContext
    {
        public PilesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PileDTO> Piles { get; set; }

        public DbSet<RuminationDTO> Ruminations { get; set; }
    }
}
