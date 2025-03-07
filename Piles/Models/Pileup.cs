using System.Collections.Generic;

namespace Piles.Models
{
    public class Pileup
    {
        public ICollection<Pile> Piles { get; set; }

        public Pileup(ICollection<Pile> piles)
        {
            Piles = piles;
        }

        public void AddPile()
        {
            List<Rumination> ruminations = new List<Rumination>();
            Pile pile = new Pile("New Pile", ruminations);
            Piles.Add(pile);
        }

        public void RemovePile(Pile pile)
        {
            Piles.Remove(pile);
        }

        public void UpdatePile(Pile pile, string title)
        {
            pile.Title = title;
        }
    }
}
