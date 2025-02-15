using Piles.Data;
using Piles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class DatabasePileCreator : IPileCreator
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public DatabasePileCreator(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreatePile(Pile pile)
        {
            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                PileDTO pileDTO = ToPileDTO(pile);

                context.Piles.Add(pileDTO);
                await context.SaveChangesAsync();
            }
        }

        public PileDTO ToPileDTO(Pile pile)
        {
            return new PileDTO()
            {
                Justification = pile.Justification,
                Ruminations = (List<RuminationDTO>)pile.Ruminations,
            };
        }
    }
}
