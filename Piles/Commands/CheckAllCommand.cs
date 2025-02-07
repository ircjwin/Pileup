using Piles.ViewModels;

namespace Piles.Commands
{
    public class CheckAllCommand : CommandBase
    {
        private readonly PileupViewModel _pileupViewModel;

        public CheckAllCommand(PileupViewModel pileupViewModel)
        {
            _pileupViewModel = pileupViewModel;
        }

        public override void Execute(object parameter)
        {
            _pileupViewModel.CheckAll();
        }
    }
}
