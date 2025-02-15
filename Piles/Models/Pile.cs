using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public string Justification { get; set; }

        // TODO: Boolean that determines if Pile tab is visible on View
        public bool IsVisible { get; set; }

        public IEnumerable<Rumination> Ruminations { get; set; }

        public Pile(string justification, IEnumerable<Rumination> ruminations)
        {
            Justification = justification;
            Ruminations = ruminations;
        }
    }
}
