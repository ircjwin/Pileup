using System;

namespace Piles.Models
{
    public class Rumination
    {
        public string Description { get; set; }

        public bool IsChecked { get; set; } = false;

        // TODO: Track how long rumination has been oustanding
        private DateTime _createdOn;
        // TODO: Track number of times rumination has been rewritten
        private int _duplicates;

        public Rumination(string description)
        {
            Description = description;
        }
    }
}
