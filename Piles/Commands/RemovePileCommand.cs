using Piles.ViewModels;

namespace Piles.Commands
{
    public class RemovePileCommand : CommandBase
    {
        private PileupViewModel _pileupViewModel;

        public RemovePileCommand(PileupViewModel pileupViewModel)
        {
            _pileupViewModel = pileupViewModel;
        }

        public override void Execute(object parameter)
        {
            PileViewModel pvm = parameter as PileViewModel;
            _pileupViewModel.RemovePile(pvm);
        }
    }
}
