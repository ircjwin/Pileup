using Piles.ViewModels;

namespace Piles.Commands
{
    public class UncheckAllCommand : CommandBase
    {
        private readonly PileupViewModel _pileupViewModel;

        public UncheckAllCommand(PileupViewModel pileupViewModel)
        {
            _pileupViewModel = pileupViewModel;
        }

        public override void Execute(object parameter)
        {
            _pileupViewModel.UncheckAll();
        }
    }
}
