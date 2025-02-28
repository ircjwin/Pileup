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
                PileEntity pileEntity = ToPileEntity(pile);

                context.Piles.Add(pileEntity);
                await context.SaveChangesAsync();
            }
        }

        public PileEntity ToPileEntity(Pile pile)
        {
            return new PileEntity()
            {
                // Creation or modification of a pile should not require list of ruminations
                Justification = pile.Justification,
                //Ruminations = (List<RuminationEntity>)pile.Ruminations,
            };
        }
    }
}
