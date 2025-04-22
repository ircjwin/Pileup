using Piles.ViewModels;

namespace Piles.Commands
{
    public class SavePileupCommand : CommandBase
    {
        public SavePileupCommand() { }

        public override void Execute(object parameter)
        {
            CommandStackViewModel.Instance.Save();
        }
    }
}
