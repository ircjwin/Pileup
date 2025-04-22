using Piles.ViewModels;
using System;

namespace Piles.Commands
{
    public abstract class UndoableCommandBase : IUndoableCommand
    {
        public abstract OperationType OperationType { get; }

        public abstract object Target { get; }

        public abstract TargetType TargetType { get; }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public abstract void Redo();

        public abstract void Undo();

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
