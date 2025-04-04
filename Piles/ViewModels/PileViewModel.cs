using Piles.Commands;
using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileViewModel : ViewModelBase
    {
        private readonly Pile _pile;

        public string Title => _pile.Title;

        private readonly ObservableCollection<RuminationViewModel> _ruminations;
        public IEnumerable<RuminationViewModel> Ruminations => _ruminations;

        private string _newRuminationDescription;
        public string NewRuminationDescription
        {
            get
            {
                return _newRuminationDescription;
            }
            set
            {
                _newRuminationDescription = value;
                OnPropertyChanged();
            }
        }

        private bool _isRemovable = true;
        public bool IsRemovable
        {
            get
            {
                return _isRemovable;
            }
            set
            {
                _isRemovable = value;
            }
        }

        private int _tabControlIndex;
        public int TabControlIndex
        {
            get
            {
                return _tabControlIndex;
            }
            set
            {
                _tabControlIndex = value;
            }
        }

        public ICommand AddRunminationCommand { get; }
        public ICommand RemoveCheckedRuminationsCommand { get; }
        public ICommand CheckAllRuminationsCommand { get; }
        public ICommand UncheckAllRuminationsCommand { get; }

        public PileViewModel(Pile pile)
        {
            _ruminations = new ObservableCollection<RuminationViewModel>();

            AddRunminationCommand = new AddRuminationCommand(pile);
            RemoveCheckedRuminationsCommand = new RemoveCheckedRuminationsCommand(pile, _ruminations);
            CheckAllRuminationsCommand = new CheckAllRuminationsCommand(_ruminations);
            UncheckAllRuminationsCommand = new UncheckAllRuminationsCommand(_ruminations);

            _pile = pile;
            _pile.PileChanged += OnPileChanged;

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

        private void OnPileChanged(Pile pile)
        {
            UpdateRuminations(pile.Ruminations);
        }
    }
}
