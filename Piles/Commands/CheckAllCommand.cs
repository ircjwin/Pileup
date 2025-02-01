using Piles.ViewModels;

namespace Piles.Commands
{
    public class CheckAllCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public CheckAllCommand(PileViewModel pileViewModel)
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            foreach (RuminationViewModel ruminationViewModel in _pileViewModel.Ruminations)
            {
                ruminationViewModel.CheckRumination();
            }
        }
    }
}
