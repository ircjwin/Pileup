namespace Piles.Commands
{
    public class SavePileupCommand : CommandBase
    {
        public SavePileupCommand() { }

        public override void Execute(object parameter)
        {
            CommandManager.Instance.Save();
        }
    }
}
