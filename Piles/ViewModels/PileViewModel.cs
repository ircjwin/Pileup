using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Piles.ViewModels
{
    public class PileViewModel : ViewModelBase
    {
        private readonly Pile _pile;

        public string Name => _pile.Name;

        private readonly ObservableCollection<RuminationViewModel> _ruminations;

        public IEnumerable<RuminationViewModel> Ruminations => _ruminations;

        public PileViewModel(Pile pile)
        {
            _pile = pile;
            _ruminations = new ObservableCollection<RuminationViewModel>();
            UpdateRuminations(_pile.Ruminations);
        }

        public void UpdateRuminations(IEnumerable<Rumination> ruminations)
        {
            _ruminations.Clear();

            foreach (Rumination rumination in ruminations)
            {
                RuminationViewModel ruminationViewModel = new RuminationViewModel(rumination);
                _ruminations.Add(ruminationViewModel);
            }
        }
    }
}
