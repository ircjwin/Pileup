namespace Piles.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public CommandStackViewModel CommandStackViewModel { get; }
        public PileupViewModel PileupViewModel { get; }
        public PileViewModel CurrentPileViewModel => PileupViewModel.TopPile;

        public MainViewModel(PileupViewModel pileupViewModel, CommandStackViewModel commandStackViewModel)
        {
            PileupViewModel = pileupViewModel;
            CommandStackViewModel = commandStackViewModel;
        }
    }
}
