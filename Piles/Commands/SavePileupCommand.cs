using Piles.ViewModels;

namespace Piles.Commands
{
    public class SavePileupCommand : CommandBase
    {
        private readonly ICommandListener _commandListener;

        public SavePileupCommand(ICommandListener commandListener)
        {
            _commandListener = commandListener;
        }

        public override void Execute(object parameter)
        {
            _commandListener.Save();
        }
    }
}
