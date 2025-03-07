using System.Collections.Generic;

namespace Piles.Models
{
    public class Pile
    {
        public string CreatedOn {  get; init; }

        public string Title { get; set; }

        public ICollection<Rumination> Ruminations { get; set; }

        public Pile(string title, ICollection<Rumination> ruminations)
        {
            Title = title;
            Ruminations = ruminations;
        }

        public void AddRumination(string description)
        {
            Rumination rumination = new Rumination(description);
            Ruminations.Add(rumination);
        }

        public void RemoveRumination(Rumination rumination)
        {
            Ruminations.Remove(rumination);
        }

        public void UpdateRumination(Rumination rumination, string description)
        {
            rumination.Description = description;
        }
    }
}
