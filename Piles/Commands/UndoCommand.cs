using System;
using System.Collections.Generic;

namespace Piles.Commands
{
    public class UndoCommand : CommandBase
    {
        private readonly Stack<IUndoableCommand> _undoStack;
        private readonly Stack<IUndoableCommand> _redoStack;

        public UndoCommand(Stack<IUndoableCommand> undoStack, Stack<IUndoableCommand> redoStack)
        {
            _undoStack = undoStack;
            _redoStack = redoStack;
        }

        //public override bool CanExecute(object parameter)
        //{
        //    return _undoStack.Count > 0;
        //}

        public override void Execute(object parameter)
        {
            if (_undoStack.Count <= 0) return;

            IUndoableCommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }
    }
}
