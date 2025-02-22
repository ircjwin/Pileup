using Piles.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Models
{
    public class Pileup
    {
        public IEnumerable<Pile> Piles { get; set; }

        private readonly IPileProvider _pileProvider;

        private readonly IPileCreator _pileCreator;

        public Pileup(IEnumerable<Pile> piles, IPileProvider pileProvider, IPileCreator pileCreator)
        {
            Piles = piles;
            _pileProvider = pileProvider;
            _pileCreator = pileCreator;
        }

        public async Task<IEnumerable<Pile>> GetAllPiles()
        {
            return await _pileProvider.GetAllPiles();
        }

        public void AddPile()
        {
            List<Rumination> ruminations = new List<Rumination>();
            Pile pile = new Pile("New Pile", ruminations);
            _pileCreator.CreatePile(pile);
        }
    }
}
