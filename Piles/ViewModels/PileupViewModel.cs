﻿using Piles.Commands;
using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileupViewModel : ViewModelBase
    {
        private bool _isInserting { get; set; } = false;

        private bool _isRemoving { get; set; } = false;
        public bool IsRemoving
        {
            get
            {
                return _isRemoving;
            }
            set
            {
                _isRemoving = value;
            }
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                if (_isInserting)
                {
                    _isInserting = false;
                    return;
                }
                if (value == _piles.Count - 1)
                {
                    if (!_isRemoving || _piles.Count == 1)
                    {
                        AddPile();
                    }
                    else
                    {
                        _currentIndex = value - 1;
                        OnPropertyChanged(nameof(CurrentIndex));
                    }
                }
                _currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private readonly Pileup _pileup;

        private readonly ObservableCollection<PileViewModel> _piles;

        public IEnumerable<PileViewModel> Piles => _piles;

        public ICommand RemovePileCommand { get; }

        public PileupViewModel(Pileup pileup)
        {
            RemovePileCommand = new RemovePileCommand(this);

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
            _piles.Add(addPileViewModel);
        }

        private void AddPile()
        {
            Pile newPile = new Pile("New List", new ObservableCollection<Rumination>());
            PileViewModel newPileViewModel = new PileViewModel(newPile);
            _isInserting = true;
            _piles.Insert(_piles.Count - 1, newPileViewModel);
            CurrentIndex = _piles.Count - 2;
        }

        public void RemovePile(PileViewModel pileViewModel)
        {
            _piles.Remove(pileViewModel);
        }
    }
}
