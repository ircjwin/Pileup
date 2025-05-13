using System.ComponentModel;

namespace Piles.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public CommandStackViewModel CommandStackViewModel { get; }
        public PileupViewModel PileupViewModel { get; }

        private PileViewModel _currentPileViewModel;
        public PileViewModel CurrentPileViewModel
        { 
            get {  return _currentPileViewModel; }
            set
            {
                _currentPileViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(PileupViewModel pileupViewModel, CommandStackViewModel commandStackViewModel)
        {
            PileupViewModel = pileupViewModel;
            CommandStackViewModel = commandStackViewModel;
            CurrentPileViewModel = PileupViewModel.TopPile;

            PileupViewModel.PropertyChanged += OnPileupViewModelPropertyChanged;
        }

        private void OnPileupViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PileupViewModel.TopPile))
            {
                CurrentPileViewModel = PileupViewModel.TopPile;
            }
        }
    }
}
