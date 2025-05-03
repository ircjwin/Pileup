using Piles.Commands;

namespace Piles.ViewModels
{
    public interface ICommandListener
    {
        void Listen(IUndoableCommand undoableCommand);
        void Save();
    }
}
