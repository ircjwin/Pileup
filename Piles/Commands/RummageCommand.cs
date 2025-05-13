using Piles.ViewModels;
using System;
using System.Collections.Generic;

namespace Piles.Commands
{
    public class RummageCommand : CommandBase
    {
        private readonly PileViewModel _pileViewModel;

        public RummageCommand(PileViewModel pileViewModel)
        {
            _pileViewModel = pileViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_pileViewModel.IsRummagePile)
            {
                IList<RuminationViewModel> rummage = new List<RuminationViewModel>();
                foreach (RuminationViewModel ruminationViewModel in _pileViewModel.Ruminations)
                {
                    ruminationViewModel.IsRummagePick = false;
                    if (ruminationViewModel.IsRummage)
                    {
                        rummage.Add(ruminationViewModel);
                    }
                }
                Random random = new Random();
                int rummageIndex = random.Next(rummage.Count);
                rummage[rummageIndex].IsRummagePick = true;
            }
        }
    }
}
