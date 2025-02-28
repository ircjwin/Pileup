using Microsoft.EntityFrameworkCore;
using Piles.Data;
using Piles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class DatabasePileProvider : IPileProvider
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public DatabasePileProvider(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Pile>> GetAllPiles()
        {
            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<PileEntity> pileEntities = await context.Piles.ToListAsync();

                return pileEntities.Select(r => ToPile(r));
            }
        }

        private static Pile ToPile(PileEntity entity)
        {
            return new Pile(entity.Justification, entity.Ruminations as IEnumerable<Rumination>);
        }
    }
}
