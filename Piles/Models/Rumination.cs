using System;

namespace Piles.Models
{
    public class Rumination
    {
        public int Origin { get; init; }

        public DateTime CreatedOn { get; init; }

        private string _description;
        public string Description
        { 
            get { return _description; }
            set
            {
                _description = value;
                OnRuminationChanged();
            }
        }

        public event Action<Rumination> RuminationChanged;

        public Rumination(int origin, DateTime createdOn, string description)
        {
            Origin = origin;
            CreatedOn = createdOn;
            Description = description;
        }

        private void OnRuminationChanged()
        {
            RuminationChanged?.Invoke(this);
        }
    }
}
