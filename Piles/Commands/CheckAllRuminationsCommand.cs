using Piles.ViewModels;
using System.Collections.ObjectModel;

namespace Piles.Commands
{
    public class CheckAllRuminationsCommand : CommandBase
    {
        private readonly ObservableCollection<RuminationViewModel> _ruminations;

        public CheckAllRuminationsCommand(ObservableCollection<RuminationViewModel> ruminations)
        {
            _ruminations = ruminations;
        }

        public override void Execute(object parameter)
        {
            foreach (RuminationViewModel ruminationViewModel in _ruminations)
            {
                ruminationViewModel.IsChecked = true;
            }
        }
    }
}
