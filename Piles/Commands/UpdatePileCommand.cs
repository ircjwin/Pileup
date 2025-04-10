using Piles.ViewModels;

namespace Piles.Commands
{
    public class UpdatePileCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public UpdatePileCommand(PileViewModel pileViewModel) 
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            _pileViewModel.IsTitleReadOnly = !_pileViewModel.IsTitleReadOnly;
            _pileViewModel.IsTitleHittable = !_pileViewModel.IsTitleHittable;
        }
    }
}
