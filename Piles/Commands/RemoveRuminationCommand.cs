using Piles.ViewModels;

namespace Piles.Commands
{
    public class RemoveRuminationCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public RemoveRuminationCommand(PileViewModel pileViewModel)
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            _pileViewModel.RemoveRumination();
        }
    }
}
