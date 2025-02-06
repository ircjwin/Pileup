using Piles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _pileupViewModel.IsRemoving = true;
            _pileupViewModel.RemovePile(pvm);
            _pileupViewModel.IsRemoving = false;
        }
    }
}
