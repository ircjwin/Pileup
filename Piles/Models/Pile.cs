using System;
using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public int Origin { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Title { get; set; }

        public ICollection<Rumination> Ruminations { get; set; }

        public event Action<Pile> PileChanged;

        public Pile(int origin, DateTime createdOn, string title, ICollection<Rumination> ruminations)
        {
            Origin = origin;
            CreatedOn = createdOn;
            Title = title;
            Ruminations = ruminations;
        }

        public void AddRumination(string description)
        {
            Rumination rumination = new Rumination(Ruminations.Count, DateTime.Now, description);
            Ruminations.Add(rumination);
            OnPileChanged();
        }

        public void RemoveRumination(Rumination rumination)
        {
            Ruminations.Remove(rumination);
            OnPileChanged();
        }

        private void OnPileChanged()
        {
            PileChanged?.Invoke(this);
        }
    }
}
