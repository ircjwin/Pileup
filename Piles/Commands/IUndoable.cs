using Piles.ViewModels;

namespace Piles.Commands
{
    public interface IUndoable
    {
        OperationType OperationType { get; }
        TargetType TargetType { get; }
        object Target { get; }
        void Undo();
        void Redo();
    }
}
