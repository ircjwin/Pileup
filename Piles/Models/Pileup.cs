using System;
using System.Collections.Generic;

namespace Piles.Models
{
    public class Pileup
    {
        public ICollection<Pile> Piles { get; set; }

        public event Action<Pileup> PileupChanged;

        public Pileup(ICollection<Pile> piles)
        {
            Piles = piles;
        }

        public void AddPile()
        {
            List<Rumination> ruminations = new List<Rumination>();
            Pile pile = new Pile(Piles.Count, DateTime.Now, "New Pile", ruminations);
            Piles.Add(pile);
            OnPileupChanged();
        }

        public void RemovePile(Pile pile)
        {
            Piles.Remove(pile);
            OnPileupChanged();
        }

        private void OnPileupChanged()
        {
            PileupChanged?.Invoke(this);
        }
    }
}
