using Piles.Services;
using System.Collections.Generic;

namespace Piles.Models
{
    public class Pileup
    {
        public IEnumerable<Pile> Piles { get; set; }

        private IPileProvider _pileProvider;
        public IPileProvider PileProvider
        {
            set
            {
                _pileProvider = value;
            }
        }

        private IPileCreator _pileCreator;
        public IPileCreator PileCreator
        {
            set
            {
                _pileCreator = value;
            }
        }

        public Pileup(IEnumerable<Pile> piles)
        {
            Piles = piles;
        }
    }
}
