using Piles.ViewModels;

namespace Piles.Commands
{
    public class RemoveRuminationCommand : CommandBase
    {
        private readonly PileupViewModel _pileupViewModel;

        public RemoveRuminationCommand(PileupViewModel pileupViewModel)
        {
            _pileupViewModel = pileupViewModel;
        }

        public override void Execute(object parameter)
        {
            _pileupViewModel.RemoveChecked();
        }
    }
}
