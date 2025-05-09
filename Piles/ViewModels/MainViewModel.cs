using System.ComponentModel;

namespace Piles.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public CommandStackViewModel CommandStackViewModel { get; }
        public PileupViewModel PileupViewModel { get; }
        public PileViewModel CurrentPileViewModel { get; private set; }

        public MainViewModel(PileupViewModel pileupViewModel, CommandStackViewModel commandStackViewModel)
        {
            PileupViewModel = pileupViewModel;
            CommandStackViewModel = commandStackViewModel;

            PileupViewModel.PropertyChanged += OnPileupViewModelPropertyChange;
            PileupViewModel.CurrentIndex = 0;
        }

        private void OnPileupViewModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PileupViewModel.CurrentIndex))
            {
                CurrentPileViewModel = PileupViewModel.GetCurrentPileViewModel();
                OnPropertyChanged(nameof(CurrentPileViewModel));
            }
        }
    }
}
