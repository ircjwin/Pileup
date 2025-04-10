using Piles.ViewModels;

namespace Piles.Commands
{
    public class UpdateRuminationCommand : CommandBase
    {
        private readonly RuminationViewModel _ruminationViewModel;

        public UpdateRuminationCommand(RuminationViewModel ruminationViewModel)
        {
            _ruminationViewModel = ruminationViewModel;
        }

        public override void Execute(object parameter)
        {
            _ruminationViewModel.IsDescriptionReadOnly = !_ruminationViewModel.IsDescriptionReadOnly;
            _ruminationViewModel.IsDescriptionHittable = !_ruminationViewModel.IsDescriptionHittable;
        }
    }
}
