using Piles.Models;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IPileCreator
    {
        Task CreatePile(Pile pile);
    }
}
