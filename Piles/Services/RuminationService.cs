using Microsoft.EntityFrameworkCore;
using Piles.Data;
using Piles.Mappers;
using Piles.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class RuminationService : IRuminationService
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public RuminationService(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateRumination(Rumination rumination)
        {
            RuminationEntity ruminationEntity = RuminationMapper.ToRuminationEntity(rumination);

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Ruminations.Add(ruminationEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteRumination(Rumination rumination)
        {
            RuminationEntity ruminationEntity = RuminationMapper.ToRuminationEntity(rumination);

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Ruminations.Remove(ruminationEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Rumination>> GetAllRuminations()
        {
            ICollection<Rumination> ruminations = new List<Rumination>();

            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                ICollection<RuminationEntity> ruminationEntities = await context.Ruminations.ToListAsync();

                foreach (RuminationEntity ruminationEntity in ruminationEntities)
                {
                    ruminations.Add(RuminationMapper.ToRumination(ruminationEntity));
                }
            }

            return ruminations;
        }

        public Task UpdateRumination(Rumination rumination)
        {
            throw new NotImplementedException();
        }
    }
}
