using Piles.Data;
using Piles.Models;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class DatabaseRuminationCreator : IRuminationCreator
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public DatabaseRuminationCreator(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateRumination(Rumination rumination, Pile pile)
        {
            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                RuminationEntity ruminationEntity = ToRuminationEntity(rumination, pile);

                context.Ruminations.Add(ruminationEntity);
                await context.SaveChangesAsync();
            }
        }

        private RuminationEntity ToRuminationEntity(Rumination rumination, Pile pile)
        {
            return new RuminationEntity()
            {
                Description = rumination.Description,
                Pile = new DatabasePileCreator(_dbContextFactory).ToPileEntity(pile),
            };
        }
    }
}
