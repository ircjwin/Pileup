using Piles.Models;
using Piles.ViewModels;

namespace Piles.Commands
{
    internal class AddRuminationCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public AddRuminationCommand(PileViewModel pileViewModel)
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            Rumination rumination = new Rumination(_pileViewModel.RuminationText);
            RuminationViewModel ruminationViewModel = new RuminationViewModel(rumination);
            _pileViewModel.RuminationText = "";
            _pileViewModel.AddRumination(ruminationViewModel);
        }
    }
}
