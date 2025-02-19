using Piles.Services;
using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public string Justification { get; set; }

        // TODO: Boolean that determines if Pile tab is visible on View
        public bool IsVisible { get; set; }

        public IEnumerable<Rumination> Ruminations { get; set; }

        private IRuminationProvider _ruminationProvider;
        public IRuminationProvider RuminationProvider
        {
            set
            {
                _ruminationProvider = value;
            }
        }

        private IRuminationCreator _ruminationCreator;
        public IRuminationCreator RuminationCreator
        {
            set
            {
                _ruminationCreator = value;
            }
        }

        public Pile(string justification, IEnumerable<Rumination> ruminations)
        {
            Justification = justification;
            Ruminations = ruminations;
        }
    }
}
