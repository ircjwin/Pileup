using Piles.Models;
using Piles.ViewModels;
using System;
using System.Linq;

namespace Piles.Commands
{
    public class InsertRuminationCommand : UndoableCommandBase
    {
        private readonly Pile _pile;

        private OperationType _operationType = OperationType.Add;
        public override OperationType OperationType
        {
            get { return _operationType; }
        }

        private Tuple<Rumination, Pile> _target;
        public override Tuple<Rumination, Pile> Target
        {
            get { return _target; }
        }

        private TargetType _targetType = TargetType.Rumination;
        public override TargetType TargetType
        {
            get { return _targetType; }
        }

        public InsertRuminationCommand(Pile pile, Tuple<Rumination, Pile> ruminationPile)
        {
            _pile = pile;
            _target = ruminationPile;
        }

        public InsertRuminationCommand(Pile pile, ICommandListener commandListener)
        {
            _pile = pile;

            commandListener.Listen(this);
        }

        public override void Execute(object parameter)
        {
            PileViewModel pileViewModel = parameter as PileViewModel;
            string newRuminationDescription = pileViewModel.NewRuminationDescription;
            _pile.AddRumination(newRuminationDescription);

            _target = new Tuple<Rumination, Pile>(_pile.Ruminations.LastOrDefault(), _pile);
            pileViewModel.NewRuminationDescription = "";

            OnExecuted();
        }

        public override void Redo()
        {
            (Rumination rumination, Pile pile) = _target;
            _pile.AddRumination(rumination);
        }

        public override void Undo()
        {
            _pile.RemoveRuminationAt(_pile.Ruminations.Count - 1);
        }

        public override InsertRuminationCommand Clone()
        {
            return new InsertRuminationCommand(_pile, _target);
        }
    }
}

