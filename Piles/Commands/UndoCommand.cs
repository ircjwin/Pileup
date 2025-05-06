using Piles.Core;

namespace Piles.Commands
{
    public class UndoCommand : CommandBase
    {
        private readonly ObservableStack<IUndoableCommand> _undoStack;
        private readonly ObservableStack<IUndoableCommand> _redoStack;

        public UndoCommand(ObservableStack<IUndoableCommand> undoStack, ObservableStack<IUndoableCommand> redoStack)
        {
            _undoStack = undoStack;
            _redoStack = redoStack;

            _undoStack.StackChanged += OnCanExecuteChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _undoStack.Count > 0 && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            if (_undoStack.Count <= 0) return;

            IUndoableCommand command = _undoStack.ObservablePop();
            command.Undo();
            _redoStack.ObservablePush(command);
        }
    }
}
