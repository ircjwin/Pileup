using Piles.Models;
using Piles.ViewModels;
using System.Linq;

namespace Piles.Commands
{
    public class InsertPileCommand : UndoableCommandBase
    {
        private readonly Pileup _pileup;

        private OperationType _operationType = OperationType.Add;
        public override OperationType OperationType
        {
            get { return _operationType; }
        }

        private Pile _target;
        public override Pile Target
        {
            get { return _target; }
        }

        private TargetType _targetType = TargetType.Pile;
        public override TargetType TargetType
        {
            get { return _targetType; }
        }

        public InsertPileCommand(Pileup pileup, Pile pile)
        {
            _pileup = pileup;
            _target = pile;
        }

        public InsertPileCommand(Pileup pileup, ICommandListener commandListener)
        {
            _pileup = pileup;

            commandListener.Listen(this);
        }

        public override void Execute(object parameter)
        {
            _pileup.AddPile();
            _target = _pileup.Piles.LastOrDefault();

            OnExecuted();
        }

        public override void Redo()
        {
            _pileup.AddPile();
        }

        public override void Undo()
        {
            _pileup.RemovePileAt(_pileup.Piles.Count - 1);
        }

        public override InsertPileCommand Clone()
        {
            return new InsertPileCommand(_pileup, _target);
        }
    }
}

