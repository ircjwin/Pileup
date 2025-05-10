﻿using Piles.Commands;
using Piles.Models;
using Piles.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class PileupViewModel : ViewModelBase
    {
        private Pileup _pileup;

        private readonly ObservableCollection<PileViewModel> _piles;
        public IEnumerable<PileViewModel> Piles => _piles;

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPileCommand { get; private set; }
        public ICommand RemovePileCommand { get; private set; }
        public ICommand SavePileupCommand { get; }

        private readonly IPileService _pileService;

        private Func<Pile, PileViewModel> _createPileViewModel;

        public PileupViewModel(Func<Pile, PileViewModel> createPileViewModel, IPileService pileService, ICommandListener commandListener)
        {
            _piles = new ObservableCollection<PileViewModel>();

            SavePileupCommand = new SavePileupCommand(commandListener);
            _createPileViewModel = createPileViewModel;
            _pileService = pileService;
        }

        // Consider Lazy<Task>(*some initialization function*) to grab everything from database
        // Lazy<task>.Value will be a Task object that needs unwrapped by 'await'
        // An async function that doesn't return and only assigns needs to be the end of the chain

        public static PileupViewModel CreateViewModel(Func<Pile, PileViewModel> createPileViewModel, IPileService pileService, ICommandListener commandListener)
        {
            PileupViewModel pileupViewModel = new PileupViewModel(createPileViewModel, pileService, commandListener);
            pileupViewModel.Load(commandListener);
            return pileupViewModel;
        }

        public async void Load(ICommandListener commandListener)
        {
            IList<Pile> piles = await _pileService.GetAllPilesAsync();
            Pileup pileup = new Pileup(piles);
            _pileup = pileup;
            _pileup.PileupChanged += OnPileupChanged;

            AddPileCommand = new AddPileCommand(_pileup, commandListener);
            RemovePileCommand = new RemovePileCommand(_pileup, commandListener);

            if (_pileup.Piles.Count == 0)
            {
                this.AddPileCommand.Execute(null);
                _pileup.Piles.FirstOrDefault().Title = "General";
            }

            UpdatePiles(_pileup.Piles);
        }

        public void UpdatePiles(IEnumerable<Pile> piles)
        {
            _piles.Clear();
            int tabControlIndex = 0;

            foreach (Pile pile in piles)
            {
                PileViewModel pileViewModel = _createPileViewModel(pile);
                pileViewModel.TabControlIndex = tabControlIndex;
                _piles.Add(pileViewModel);

                tabControlIndex++;
            }
        }

        public PileViewModel GetCurrentPileViewModel()
        {
            if (_currentIndex < 0  || _currentIndex >= _piles.Count)
            {
                return null;
            }

            return _piles[_currentIndex];
        }

        private void OnPileupChanged(Pileup pileup)
        {
            UpdatePiles(pileup.Piles);
        }
    }
}
