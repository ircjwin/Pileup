namespace Piles.Data
{
    public interface IPilesDbContextFactory
    {
        PilesDbContext CreateDbContext();
    }
}
