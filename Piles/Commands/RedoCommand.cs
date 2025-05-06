using Piles.Core;

namespace Piles.Commands
{
    public class RedoCommand : CommandBase
    {
        private readonly ObservableStack<IUndoableCommand> _undoStack;
        private readonly ObservableStack<IUndoableCommand> _redoStack;

        public RedoCommand(ObservableStack<IUndoableCommand> undoStack, ObservableStack<IUndoableCommand> redoStack)
        {
            _undoStack = undoStack;
            _redoStack = redoStack;

            _redoStack.StackChanged += OnCanExecuteChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _redoStack.Count > 0 && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            if (_redoStack.Count <= 0) return;

            IUndoableCommand command = _redoStack.ObservablePop();
            command.Redo();
            _undoStack.ObservablePush(command);
        }
    }
}
