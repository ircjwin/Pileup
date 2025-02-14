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
                RuminationDTO ruminationDTO = ToRuminationDTO(rumination, pile);

                context.Ruminations.Add(ruminationDTO);
                await context.SaveChangesAsync();
            }
        }

        private RuminationDTO ToRuminationDTO(Rumination rumination, Pile pile)
        {
            return new RuminationDTO()
            {
                Description = rumination.Description,
                Pile = new DatabasePileCreator(_dbContextFactory).ToPileDTO(pile),
            };
        }
    }
}
