namespace Piles.DbContexts
{
    public interface IPilesDbContextFactory
    {
        PilesDbContext CreateDbContext();
    }
}
