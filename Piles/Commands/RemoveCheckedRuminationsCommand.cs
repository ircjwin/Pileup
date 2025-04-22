using Piles.Models;
using Piles.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Piles.Commands
{
    public class RemoveCheckedRuminationsCommand : UndoableCommandBase
    {
        private readonly Pile _pile;
        private readonly ICollection<RuminationViewModel> _ruminationViewModels;
        private ICollection<(int, Rumination)> _removedRuminations;

        private OperationType _operationType = OperationType.Remove;
        public override OperationType OperationType
        {
            get { return _operationType; }
        }

        private ICollection<(Rumination, Pile)> _target;
        public override ICollection<(Rumination, Pile)> Target
        {
            get { return _target; }
        }

        private TargetType _targetType = TargetType.RuminationCollection;
        public override TargetType TargetType
        {
            get { return _targetType; }
        }

        public RemoveCheckedRuminationsCommand(Pile pile, ICollection<(int, Rumination)> removedRuminations, ICollection<(Rumination, Pile)> target)
        {
            _pile = pile;
            _removedRuminations = removedRuminations;
            _target = target;
        }

        public RemoveCheckedRuminationsCommand(Pile pile, ICollection<RuminationViewModel> ruminations)
        {
            _pile = pile;
            _ruminationViewModels = ruminations;
        }

        public override void Execute(object parameter)
        {
            _removedRuminations = new List<(int, Rumination)>();
            _target = new List<(Rumination, Pile)>();
            int iterCount = _pile.Ruminations.Count - 1;
            var ruminationMVMs = _pile.Ruminations.Zip(_ruminationViewModels, (m, vm) => (Model: m, ViewModel: vm));

            foreach (var ruminationMVM in ruminationMVMs.Reverse())
            {
                if (ruminationMVM.ViewModel.IsChecked)
                {
                    _target.Add((ruminationMVM.Model, _pile));
                    _removedRuminations.Add((iterCount, ruminationMVM.Model));
                    _pile.RemoveRumination(ruminationMVM.Model);
                }
                iterCount--;
            }

            CommandStackViewModel.Instance.AddCommand(this.Clone());
        }

        public override void Redo()
        {
            foreach ((int ruminationIndex, Rumination removedRumination) in _removedRuminations)
            {
                int iterCount = _pile.Ruminations.Count - 1;

                foreach (Rumination rumination in _pile.Ruminations.Reverse())
                {
                    if (iterCount == ruminationIndex)
                    {
                        _pile.RemoveRumination(removedRumination);
                        break;
                    }
                    iterCount--;
                }
            }
        }

        public override void Undo()
        {
            IList<Rumination> ruminations = _pile.Ruminations.ToList();

            foreach ((int ruminationIndex, Rumination removedRumination) in _removedRuminations.Reverse())
            {
                ruminations.Insert(ruminationIndex, removedRumination);
            }
            _pile.Ruminations = ruminations;
        }

        public RemoveCheckedRuminationsCommand Clone()
        {
            return new RemoveCheckedRuminationsCommand(_pile, _removedRuminations, _target);
        }
    }
}
