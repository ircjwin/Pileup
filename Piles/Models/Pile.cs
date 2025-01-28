using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public string Name { get; set; }

        // TODO: Boolean that determines if Pile tab is visible on View
        public bool IsVisible { get; set; }

        public IEnumerable<Rumination> Ruminations { get; set; }

        public Pile(string name, IEnumerable<Rumination> ruminations)
        {
            Name = name;
            Ruminations = ruminations;
        }
    }
}
