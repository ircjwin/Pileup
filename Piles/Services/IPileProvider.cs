using Piles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IPileProvider
    {
        Task<IEnumerable<Pile>> GetAllPiles();
    }
}
