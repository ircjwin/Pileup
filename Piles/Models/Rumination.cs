using System;

namespace Piles.Models
{
    public class Rumination
    {
        public int Origin { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Description { get; set; }

        public Rumination(int origin, DateTime createdOn, string description)
        {
            Origin = origin;
            CreatedOn = createdOn;
            Description = description;
        }
    }
}
