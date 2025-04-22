using Piles.Models;
using Piles.ViewModels;

namespace Piles.Commands
{
    public class UpdatePileTitleCommand : UndoableCommandBase
    {
        private OperationType _operationType = OperationType.Modify;
        public override OperationType OperationType
        {
            get { return _operationType; }
        }

        private Pile _target;
        public override object Target
        {
            get { return _target; }
        }

        private TargetType _targetType = TargetType.Pile;
        public override TargetType TargetType
        {
            get { return _targetType; }
        }

        private string _oldTitle;
        private string _newTitle;

        public UpdatePileTitleCommand(Pile pile)
        {
            _target = pile;
            _oldTitle = pile.Title;
        }

        public UpdatePileTitleCommand(Pile pile, string newTitle)
        {
            _target = pile;
            _oldTitle = pile.Title;
            _newTitle = newTitle;
        }

        public override void Execute(object parameter)
        {
            PileViewModel pileViewModel = parameter as PileViewModel;
            _newTitle = pileViewModel.Title;
            _target.Title = _newTitle;
            pileViewModel.UpdatePileCommand.Execute(null);
            CommandStackViewModel.Instance.AddCommand(this.Clone());
        }

        public override void Redo()
        {
            _target.Title = _newTitle;
        }

        public override void Undo()
        {
            _target.Title = _oldTitle;
        }

        public UpdatePileTitleCommand Clone()
        {
            return new UpdatePileTitleCommand(_target, _newTitle);
        }
    }
}
