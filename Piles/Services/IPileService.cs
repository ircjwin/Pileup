using Piles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IPileService
    {
        Task CreatePile();
        Task<Pile> GetPileById(int id);
        Task<IEnumerable<Pile>> GetAllPiles();
        Task UpdatePile(Pile pile);
        Task DeletePile(Pile pile);
    }
}
