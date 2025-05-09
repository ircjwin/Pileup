﻿using Piles.Models;
using Piles.ViewModels;

namespace Piles.Commands
{
    public class RemovePileCommand : UndoableCommandBase
    {
        private readonly Pileup _pileup;
        private int _pileIndex;

        private OperationType _operationType = OperationType.Remove;
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

        public RemovePileCommand(Pileup pileup, Pile pile, int pileIndex)
        {
            _pileup = pileup;
            _target = pile;
            _pileIndex = pileIndex;
        }

        public RemovePileCommand(Pileup pileup, ICommandListener commandListener)
        {
            _pileup = pileup;

            commandListener.Listen(this);
        }

        public override void Execute(object parameter)
        {
            int iterCount = 0;
            PileViewModel pileViewModel = parameter as PileViewModel;
            _pileIndex = pileViewModel.TabControlIndex;

            foreach (Pile pile in _pileup.Piles)
            {
                if (iterCount == _pileIndex)
                {
                    _target = pile;
                    _pileup.RemovePile(pile);
                    break;
                }
                iterCount++;
            }

            OnExecuted();
        }

        public override void Redo()
        {
            int iterCount = 0;

            foreach (Pile pile in _pileup.Piles)
            {
                if (iterCount == _pileIndex)
                {
                    _pileup.RemovePile(pile);
                    break;
                }
                iterCount++;
            }
        }

        public override void Undo()
        {
            _pileup.InsertPile(_pileIndex, _target);
        }

        public override RemovePileCommand Clone()
        {
            return new RemovePileCommand(_pileup, _target, _pileIndex);
        }
    }
}
