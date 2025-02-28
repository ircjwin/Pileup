using Piles.Data;
using Piles.Models;
using Piles.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class PileService : IPileService
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public PileService(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreatePile()
        {
            Pile pile = new Pile("New Pile", new List<Rumination>());
            PileEntity pileEntity = PileMapper.ToPileEntity(pile);

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Piles.Add(pileEntity);
                await context.SaveChangesAsync();
            }
        }

        public Task DeletePile(Pile pile)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pile>> GetAllPiles()
        {
            throw new NotImplementedException();
        }

        public Task<Pile> GetPileById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePile(Pile pile)
        {
            throw new NotImplementedException();
        }
    }
}
