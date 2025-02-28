using Piles.Stores;
using Piles.ViewModels;
using System.Threading.Tasks;

namespace Piles.Commands
{
    internal class CreatePileCommand : AsyncCommandBase
    {
        private readonly PileupViewModel _pileupViewModel;

        private readonly PilesStore _pilesStore;

        public CreatePileCommand(PileupViewModel pileupViewModel, PilesStore pilesStore)
        {
            _pileupViewModel = pileupViewModel;
            _pilesStore = pilesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _pilesStore.MakePile();
            _pileupViewModel.UpdatePiles(_pilesStore.Piles);
        }
    }
}
