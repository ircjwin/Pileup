using Piles.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Piles.ViewModels
{
    public class PileupViewModel : ViewModelBase
    {
        private readonly Pileup _pileup;

        private readonly ObservableCollection<PileViewModel> _piles;

        public IEnumerable<PileViewModel> Piles => _piles;

        public PileupViewModel(Pileup pileup)
        {
            _pileup = pileup;
            _piles = new ObservableCollection<PileViewModel>();
            UpdatePiles(_pileup.Piles);
        }

        private void UpdatePiles(IEnumerable<Pile> piles)
        {
            _piles.Clear();

            foreach (Pile pile in piles)
            {
                PileViewModel pileViewModel = new PileViewModel(pile);
                _piles.Add(pileViewModel);
            }
        }
    }
}
