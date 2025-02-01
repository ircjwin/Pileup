using Piles.ViewModels;

namespace Piles.Commands
{
    public class UncheckAllCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public UncheckAllCommand(PileViewModel pileViewModel)
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            foreach (RuminationViewModel ruminationViewModel in _pileViewModel.Ruminations)
            {
                ruminationViewModel.UncheckRumination();
            }
        }
    }
}
