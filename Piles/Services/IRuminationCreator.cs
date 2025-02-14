using Piles.Models;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IRuminationCreator
    {
        Task CreateRumination(Rumination Rumination, Pile pile);
    }
}
