using System.Windows.Input;

namespace Piles.Commands
{
    public interface IUndoableCommand : ICommand
    {
        OperationType OperationType { get; }
        TargetType TargetType { get; }
        object Target { get; }
        void Undo();
        void Redo();
    }
}
