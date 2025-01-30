using Piles.Commands;
using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileViewModel : ViewModelBase
    {
        private string _ruminationText;
        public string RuminationText
        {
            get
            {
                return _ruminationText;
            }
            set
            {
                _ruminationText = value;
                OnPropertyChanged(nameof(RuminationText));
            }
        }

        private readonly Pile _pile;

        public string Name => _pile.Name;

        private readonly ObservableCollection<RuminationViewModel> _ruminations;

        public IEnumerable<RuminationViewModel> Ruminations => _ruminations;

        public ICommand AddRunminationCommand { get; }

        public PileViewModel(Pile pile)
        {
            AddRunminationCommand = new AddRuminationCommand(this);

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

        public void AddRumination(RuminationViewModel ruminationViewModel)
        {
            _ruminations.Add(ruminationViewModel);
        }
    }
}
