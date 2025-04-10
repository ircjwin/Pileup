﻿using Microsoft.EntityFrameworkCore;
using Piles.Commands;
using Piles.DbContexts;
using Piles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class RuminationService : IRuminationService
    {
        private readonly IPilesDbContextFactory _pilesDbContextFactory;

        public RuminationService(IPilesDbContextFactory pilesDbContextFactory)
        {
            _pilesDbContextFactory = pilesDbContextFactory;
        }

        public async Task<ICollection<Rumination>> GetRuminationsByPileKeyAsync(int pileOrigin, DateTime pileCreatedOn)
        {
            ICollection<RuminationDb> ruminationDbs;
            ICollection<Rumination> ruminations = new List<Rumination>();

            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                ruminationDbs = await pilesDbContext.Ruminations
                    .Where(c => c.PileOrigin == pileOrigin && c.PileCreatedOn == pileCreatedOn)
                    .AsNoTracking()
                .ToListAsync();
            }

            foreach (RuminationDb ruminationDb in ruminationDbs)
            {
                ruminations.Add(ToDomain(ruminationDb));
            }

            return ruminations;
        }

        public async Task<Rumination> GetRuminationByKeyAsync(int origin, DateTime createdOn)
        {
            RuminationDb ruminationDb;

            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                ruminationDb = await pilesDbContext.Ruminations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Origin == origin && c.CreatedOn == createdOn);
            }

            return ToDomain(ruminationDb);
        }

        public async Task<RuminationDb> GetRuminationByKeyAsync(int origin, DateTime createdOn, PilesDbContext pilesDbContext)
        {
            return await pilesDbContext.Ruminations
                .FirstOrDefaultAsync(c => c.Origin == origin && c.CreatedOn == createdOn);
        }

        public void CreateRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext)
        {
            pilesDbContext.Ruminations.Add(ToDb(rumination, pile));
        }

        public async void UpdateRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext)
        {
            RuminationDb ruminationDb = await GetRuminationByKeyAsync(rumination.Origin, rumination.CreatedOn, pilesDbContext);
            ruminationDb.Description = rumination.Description;
        }

        public void DeleteRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext)
        {
            pilesDbContext.Ruminations.Remove(ToDb(rumination, pile));
        }

        public void Save(ICollection<(OperationType, (Rumination, Pile))> unsavedRuminations)
        {
            using (PilesDbContext pilesDbContext = _pilesDbContextFactory.CreateDbContext())
            {
                foreach ((OperationType operationType, (Rumination rumination, Pile pile)) in unsavedRuminations)
                {
                    switch (operationType)
                    {
                        case (OperationType.Add):
                            CreateRumination(rumination, pile, pilesDbContext);
                            break;
                        case (OperationType.Modify):
                            UpdateRumination(rumination, pile, pilesDbContext);
                            break;
                        case (OperationType.Remove):
                            DeleteRumination(rumination, pile, pilesDbContext);
                            break;
                    }
                }
                pilesDbContext.SaveChangesAsync();
            }
        }

        private Rumination ToDomain(RuminationDb ruminationDb)
        {
            return new Rumination(ruminationDb.Origin, ruminationDb.CreatedOn, ruminationDb.Description);
        }

        private RuminationDb ToDb(Rumination rumination, Pile pile)
        {
            return new RuminationDb()
            {
                Origin = rumination.Origin,
                CreatedOn = rumination.CreatedOn,
                Description = rumination.Description,

                PileOrigin = pile.Origin,
                PileCreatedOn = pile.CreatedOn,
            };
        }
    }
}
