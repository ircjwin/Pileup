using Piles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IRuminationService
    {
        Task CreateRumination(Rumination rumination);
        Task<Rumination> GetRuminationById(int id);
        Task<IEnumerable<Rumination>> GetAllRuminations();
        Task UpdateRumination(Rumination rumination);
        Task DeleteRumination(Rumination rumination);
    }
}
