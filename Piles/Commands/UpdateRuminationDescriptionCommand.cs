using Piles.Models;
using Piles.ViewModels;
using System;

namespace Piles.Commands
{
    public class UpdateRuminationDescriptionCommand : UndoableCommandBase
    {
        private OperationType _operationType = OperationType.Modify;
        public override OperationType OperationType
        {
            get { return _operationType; }
        }

        private Tuple<Rumination, Pile> _target;
        public override object Target
        {
            get { return _target; }
        }

        private TargetType _targetType = TargetType.Rumination;
        public override TargetType TargetType
        {
            get { return _targetType; }
        }

        private string _oldDescription;
        private string _newDescription;

        public UpdateRuminationDescriptionCommand(Rumination rumination)
        {
            Pile pile = new Pile(default, default, default, default);
            _target = Tuple.Create(rumination, pile);
            _oldDescription = rumination.Description;
        }

        public UpdateRuminationDescriptionCommand(Tuple<Rumination, Pile> ruminationPile, string newDescription)
        {
            _target = ruminationPile;
            _oldDescription = ruminationPile.Item1.Description;
            _newDescription = newDescription;
        }

        public override void Execute(object parameter)
        {
            RuminationViewModel ruminationViewModel = parameter as RuminationViewModel;
            _newDescription = ruminationViewModel.Description;
            _target.Item1.Description = _newDescription;
            ruminationViewModel.UpdateRuminationCommand.Execute(null);
            CommandManager.Instance.AddCommand(this.Clone());
        }

        public override void Redo()
        {
            _target.Item1.Description = _newDescription;
        }

        public override void Undo()
        {
            _target.Item1.Description = _oldDescription;
        }

        public UpdateRuminationDescriptionCommand Clone()
        {
            return new UpdateRuminationDescriptionCommand(_target, _newDescription);
        }
    }
}
