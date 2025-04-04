using Piles.Models;
using System.Collections.Generic;
using System.Linq;

namespace Piles.Commands
{
    public class AddPileCommand : UndoableCommandBase
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

        public AddPileCommand(Pileup pileup, Pile pile)
        {
            _pileup = pileup;
            _target = pile;
        }

        public AddPileCommand(Pileup pileup)
        {
            _pileup = pileup;
        }

        public override void Execute(object parameter)
        {
            _pileup.AddPile();
            _target = _pileup.Piles.LastOrDefault();

            CommandManager.Instance.AddCommand(this.Clone());
        }

        public override void Redo()
        {
            _pileup.AddPile();
        }

        public override void Undo()
        {
            IList<Pile> piles = _pileup.Piles.ToList<Pile>();
            int pileIndex = piles.Count - 2;
            _pileup.RemovePile(piles[pileIndex]);
        }

        public AddPileCommand Clone()
        {
            return new AddPileCommand(_pileup, _target);
        }
    }
}
