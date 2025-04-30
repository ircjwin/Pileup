using Piles.ViewModels;

namespace Piles.Commands
{
    public abstract class UndoableCommandBase : CommandBase, IUndoable
    {
        public abstract OperationType OperationType { get; }

        public abstract object Target { get; }

        public abstract TargetType TargetType { get; }

        public abstract void Redo();

        public abstract void Undo();
    }
}
