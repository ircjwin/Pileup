using System;

namespace Piles.Models
{
    public class Rumination
    {
        public string Description { get; set; }

        // TODO: Track how long rumination has been oustanding
        public DateTime CreatedOn {  get; init; }

        // TODO: Track number of times rumination has been rewritten
        public uint Duplicates { get; set; }

        public Rumination(string description, uint duplicates = default, DateTime createdOn = default)
        {
            Description = description;
            Duplicates = duplicates;
            CreatedOn = createdOn;
        }
    }
}
