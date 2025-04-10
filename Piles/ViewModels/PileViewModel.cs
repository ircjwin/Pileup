using Piles.Commands;
using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileViewModel : ViewModelBase
    {
        private bool _isTitleReadOnly = true;
        public bool IsTitleReadOnly
        {
            get { return _isTitleReadOnly; }
            set
            { 
                _isTitleReadOnly = value;
                OnPropertyChanged();
            }
        }

        private bool _isTitleHittable = false;
        public bool IsTitleHittable
        {
            get { return _isTitleHittable; }
            set
            { 
                _isTitleHittable = value;
                OnPropertyChanged();
            }
        }

        private readonly Pile _pile;

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

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
        public ICommand UpdatePileCommand { get; }
        public ICommand UpdatePileTitleCommand { get; }

        public PileViewModel(Pile pile)
        {
            _pile = pile;
            _title = pile.Title;
            _pile.PileChanged += OnPileChanged;
            _ruminations = new ObservableCollection<RuminationViewModel>();

            AddRunminationCommand = new AddRuminationCommand(_pile);
            RemoveCheckedRuminationsCommand = new RemoveCheckedRuminationsCommand(_pile, _ruminations);
            CheckAllRuminationsCommand = new CheckAllRuminationsCommand(_ruminations);
            UncheckAllRuminationsCommand = new UncheckAllRuminationsCommand(_ruminations);
            UpdatePileCommand = new UpdatePileCommand(this);
            UpdatePileTitleCommand = new UpdatePileTitleCommand(_pile);

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
