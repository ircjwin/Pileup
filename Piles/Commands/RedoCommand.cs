using System;
using System.Collections.Generic;

namespace Piles.Commands
{
    public class RedoCommand : CommandBase
    {
        private readonly Stack<IUndoableCommand> _undoStack;
        private readonly Stack<IUndoableCommand> _redoStack;

        public RedoCommand(Stack<IUndoableCommand> undoStack, Stack<IUndoableCommand> redoStack)
        {
            _undoStack = undoStack;
            _redoStack = redoStack;
        }

        //public override bool CanExecute(object parameter)
        //{
        //    return _redoStack.Count > 0;
        //}

        public override void Execute(object parameter)
        {
            if (_redoStack.Count <= 0) return;

            IUndoableCommand command = _redoStack.Pop();
            command.Redo();
            _undoStack.Push(command);
        }
    }
}
