namespace Piles.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public CommandStackViewModel CommandStackViewModel { get; }
        public PileupViewModel PileupViewModel { get; }

        public MainViewModel(PileupViewModel pileupViewModel, CommandStackViewModel commandStackViewModel)
        {
            PileupViewModel = pileupViewModel;
            CommandStackViewModel = commandStackViewModel;
        }
    }
}
