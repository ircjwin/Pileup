using System;
using System.Collections.Generic;

namespace Piles.Models
{
    public class Pileup
    {
        public IList<Pile> Piles { get; set; }

        public event Action<Pileup> PileupChanged;

        public Pileup(IList<Pile> piles)
        {
            Piles = piles;
        }

        public void AddPile()
        {
            IList<Rumination> ruminations = new List<Rumination>();
            Pile pile = new Pile(Piles.Count, DateTime.Now, "New Pile", ruminations);
            Piles.Add(pile);
            OnPileupChanged();
        }

        public void AddPile(Pile pile)
        {
            Piles.Add(pile);
            OnPileupChanged();
        }

        public void InsertPile(int pileIndex, Pile pile)
        {
            Piles.Insert(pileIndex, pile);
            OnPileupChanged();
        }

        public void RemovePile(Pile pile)
        {
            Piles.Remove(pile);
            OnPileupChanged();
        }

        public void RemovePileAt(int pileIndex)
        {
            Piles.RemoveAt(pileIndex);
            OnPileupChanged();
        }

        private void OnPileupChanged()
        {
            PileupChanged?.Invoke(this);
        }
    }
}
