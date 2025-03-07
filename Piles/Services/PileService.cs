using Microsoft.EntityFrameworkCore;
using Piles.Data;
using Piles.Mappers;
using Piles.Models;
using System;
using System.Collections.Generic;
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

        public async Task DeletePile(Pile pile)
        {
            PileEntity pileEntity = PileMapper.ToPileEntity(pile);

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Piles.Remove(pileEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Pile>> GetAllPiles()
        {
            ICollection<Pile> piles = new List<Pile>();

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                ICollection<PileEntity> pileEntities = await context.Piles.ToListAsync();

                foreach (PileEntity pileEntity in pileEntities)
                {
                    piles.Add(PileMapper.ToPile(pileEntity));
                }
            }

            return piles;
        }

        public Task UpdatePile(Pile pile)
        {
            throw new NotImplementedException();
        }
    }
}
