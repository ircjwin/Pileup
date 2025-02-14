using Piles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IRuminationProvider
    {
        Task<IEnumerable<Rumination>> GetAllRuminations();
    }
}
