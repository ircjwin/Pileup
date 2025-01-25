using Piles.Models;

namespace Piles.ViewModels
{
    public class RuminationViewModel : ViewModelBase
    {
        private readonly Rumination _rumination;

        public string Description => _rumination.Description;

        public bool IsChecked => _rumination.IsChecked;

        public RuminationViewModel(Rumination rumination)
        {
            _rumination = rumination;
        }
    }
}
