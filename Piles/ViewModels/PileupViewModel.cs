using Piles.Commands;
using Piles.Models;
using Piles.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Piles.ViewModels
{
    internal class PileupViewModel : ViewModelBase
    {
        private Pileup _pileup;

        private readonly ObservableCollection<PileViewModel> _piles;
        public IEnumerable<PileViewModel> Piles => _piles;

        private bool _isUpdating { get; set; } = false;

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                if (_isUpdating) return;

                _currentIndex = value;

                if (_currentIndex == _piles.Count - 1)
                {
                    this.AddPileCommand.Execute(null);
                }
            }
        }

        public ICommand AddPileCommand { get; private set; }
        public ICommand RemovePileCommand { get; private set; }
        public ICommand SavePileupCommand { get; }

        private readonly IPileService _pileService;

        public PileupViewModel(IPileService pileService)
        {
            _piles = new ObservableCollection<PileViewModel>();

            SavePileupCommand = new SavePileupCommand();
            _pileService = pileService;
        }

        // Consider Lazy<Task>(*some initialization function*) to grab everything from database
        // Lazy<task>.Value will be a Task object that needs unwrapped by 'await'
        // An async function that doesn't return and only assigns needs to be the end of the chain

        public static PileupViewModel CreateViewModel(IPileService pileService)
        {
            PileupViewModel pileupViewModel = new PileupViewModel(pileService);
            pileupViewModel.Load();
            return pileupViewModel;
        }

        public async void Load()
        {
            IList<Pile> piles = await _pileService.GetAllPilesAsync();
            Pileup pileup = new Pileup(piles);
            _pileup = pileup;
            _pileup.PileupChanged += OnPileupChanged;

            AddPileCommand = new AddPileCommand(_pileup);
            RemovePileCommand = new RemovePileCommand(_pileup);

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
                PileViewModel pileViewModel = new PileViewModel(pile);
                pileViewModel.TabControlIndex = tabControlIndex;
                _piles.Add(pileViewModel);

                tabControlIndex++;
            }

            Pile addPile = new Pile(-1, default, "+", new ObservableCollection<Rumination>());
            PileViewModel addPileViewModel = new PileViewModel(addPile);
            addPileViewModel.IsRemovable = false;
            _piles.Add(addPileViewModel);
        }

        private void OnPileupChanged(Pileup pileup)
        {
            _isUpdating = true;
            UpdatePiles(pileup.Piles);
            _isUpdating = false;
        }
    }
}
