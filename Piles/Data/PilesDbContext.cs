using Microsoft.EntityFrameworkCore;

namespace Piles.Data
{
    public class PilesDbContext : DbContext
    {
        public PilesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PileEntity> Piles { get; set; }

        public DbSet<RuminationEntity> Ruminations { get; set; }
    }
}
