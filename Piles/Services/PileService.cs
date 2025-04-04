using Microsoft.EntityFrameworkCore;
using Piles.Commands;
using Piles.DbContexts;
using Piles.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class PileService : IPileService
    {
        private readonly IPilesDbContextFactory _pilesDbContextFactory;

        public PileService(IPilesDbContextFactory pilesDbContextFactory)
        {
            _pilesDbContextFactory = pilesDbContextFactory;
        }

        public async Task<ICollection<Pile>> GetAllPilesAsync()
        {
            ICollection<PileDb> pileDbs;
            ICollection<Pile> piles = new List<Pile>();

            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                pileDbs = await pilesDbContext.Piles
                    .Include(b => b.Ruminations)
                    .AsNoTracking()
                    .ToListAsync();
            }

            foreach (PileDb pileDb in pileDbs)
            {
                piles.Add(ToDomain(pileDb));
            }

            return piles;
        }

        public async Task<Pile> GetPileByKeyAsync(int origin, DateTime createdOn)
        {
            PileDb pileDb;

            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                pileDb = await pilesDbContext.Piles
                    .Include(b => b.Ruminations)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Origin == origin && b.CreatedOn == createdOn);
            }

            return ToDomain(pileDb);
        }

        public void CreatePile(Pile pile, PilesDbContext pilesDbContext)
        {
            pilesDbContext.Piles.Add(ToDb(pile));
        }

        public void UpdatePile(Pile pile, PilesDbContext pilesDbContext)
        {
            pilesDbContext.Piles.Entry(ToDb(pile)).State = EntityState.Modified;
        }

        public void DeletePile(Pile pile, PilesDbContext pilesDbContext)
        {
            pilesDbContext.Piles.Remove(ToDb(pile));
        }

        public void Save(ICollection<(OperationType, Pile)> unsavedPiles)
        {
            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                foreach ((OperationType operationType, Pile pile) in unsavedPiles)
                {
                    switch (operationType)
                    {
                        case (OperationType.Add):
                            CreatePile(pile, pilesDbContext);
                            break;
                        case (OperationType.Modify):
                            UpdatePile(pile, pilesDbContext);
                            break;
                        case (OperationType.Remove):
                            DeletePile(pile, pilesDbContext);
                            break;
                    }
                }
                pilesDbContext.SaveChangesAsync();
            }
        }

        private Pile ToDomain(PileDb pileDb)
        {
            ICollection<Rumination> ruminations = new List<Rumination>();

            foreach (RuminationDb ruminationDb in pileDb.Ruminations)
            {
                Rumination rumination = new Rumination(ruminationDb.Origin, ruminationDb.CreatedOn, ruminationDb.Description);
                ruminations.Add(rumination);
            }

            return new Pile(pileDb.Origin, pileDb.CreatedOn, pileDb.Title, ruminations);
        }

        private PileDb ToDb(Pile pile)
        {
            return new PileDb()
            {
                Origin = pile.Origin,
                CreatedOn = pile.CreatedOn,
                Title = pile.Title,
            };
        }
    }
}
