using Piles.Commands;
using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileupViewModel : ViewModelBase
    {
        private bool _isInserting { get; set; } = false;

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                if (value == -1 && _piles.Count > 1)
                {
                    return;
                }
                if (_isInserting)
                {
                    _isInserting = false;
                    return;
                }
                if (value == _piles.Count - 1)
                {
                    AddPile();
                }
                _currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private readonly Pileup _pileup;

        private readonly ObservableCollection<PileViewModel> _piles;

        public IEnumerable<PileViewModel> Piles => _piles;

        public ICommand CreatePileCommand { get; set; }

        public ICommand RemovePileCommand { get; }

        public ICommand RemoveRuminationCommand { get; }

        public ICommand CheckAllCommand { get; }

        public ICommand UncheckAllCommand { get; }

        public PileupViewModel(Pileup pileup)
        {
            CreatePileCommand = new CreatePileCommand(pileup);
            RemovePileCommand = new RemovePileCommand(this);
            RemoveRuminationCommand = new RemoveRuminationCommand(this);
            CheckAllCommand = new CheckAllCommand(this);
            UncheckAllCommand = new UncheckAllCommand(this);

            _pileup = pileup;
            _piles = new ObservableCollection<PileViewModel>();
            UpdatePiles(_pileup.Piles);
        }

        private void UpdatePiles(IEnumerable<Pile> piles)
        {
            _piles.Clear();

            foreach (Pile pile in piles)
            {
                PileViewModel pileViewModel = new PileViewModel(pile);
                _piles.Add(pileViewModel);
            }

            Pile addPile = new Pile("+", new ObservableCollection<Rumination>());
            PileViewModel addPileViewModel = new PileViewModel(addPile);
            addPileViewModel.IsRemovable = false;
            _piles.Add(addPileViewModel);
        }

        private void AddPile()
        {
            Pile newPile = new Pile("New Pile", new ObservableCollection<Rumination>());
            PileViewModel newPileViewModel = new PileViewModel(newPile);
            _isInserting = true;
            _piles.Insert(_piles.Count - 1, newPileViewModel);
            CurrentIndex = _piles.Count - 2;
        }

        public void RemovePile(PileViewModel pileViewModel)
        {
            _piles.Remove(pileViewModel);
        }

        public void RemoveChecked()
        {
            _piles[CurrentIndex].RemoveRumination();
        }

        public void CheckAll()
        {
            foreach (RuminationViewModel ruminationViewModel in _piles[CurrentIndex].Ruminations)
            {
                ruminationViewModel.CheckRumination();
            }
        }

        public void UncheckAll()
        {
            foreach (RuminationViewModel ruminationViewModel in _piles[CurrentIndex].Ruminations)
            {
                ruminationViewModel.UncheckRumination();
            }
        }
    }
}
