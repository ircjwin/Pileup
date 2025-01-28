using System.Collections.Generic;

namespace Piles.Models
{
    public class Pileup
    {
        public IEnumerable<Pile> Piles { get; set; }

        public Pileup(IEnumerable<Pile> piles)
        {
            Piles = piles;
        }
    }
}
