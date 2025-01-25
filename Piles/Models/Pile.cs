using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public string Name { get; set; }

        public IEnumerable<Rumination> Ruminations { get; set; }

        public Pile(string name, IEnumerable<Rumination> ruminations)
        {
            Name = name;
            Ruminations = ruminations;
        }
    }
}
