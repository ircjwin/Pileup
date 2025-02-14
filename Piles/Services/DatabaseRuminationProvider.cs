using Microsoft.EntityFrameworkCore;
using Piles.Data;
using Piles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piles.Services
{
    public class DatabaseRuminationProvider : IRuminationProvider
    {
        private readonly IPilesDbContextFactory _dbContextFactory;

        public DatabaseRuminationProvider(IPilesDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Rumination>> GetAllRuminations()
        {
            using (PilesDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<RuminationDTO> ruminationDTOs = await context.Ruminations.ToListAsync();

                return ruminationDTOs.Select(r => ToRumination(r));
            }
        }

        private static Rumination ToRumination(RuminationDTO dto)
        {
            return new Rumination(dto.Description);
        }
    }
}
