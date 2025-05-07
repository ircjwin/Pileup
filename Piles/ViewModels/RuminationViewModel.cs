using Piles.Commands;
using Piles.Models;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class RuminationViewModel : ViewModelBase
    {
        private bool _isEditing = false;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotEditing));
            }
        }

        public bool IsNotEditing
        {
            get { return !_isEditing; }
        }

        private readonly Rumination _rumination;

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecked = false;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isRollable = false;
        public bool IsRollable
        {
            get { return  IsRollable; }
            set
            {
                _isRollable = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateRuminationCommand { get; }
        public ICommand UpdateRuminationDescriptionCommand { get; }

        public RuminationViewModel(Rumination rumination, Pile pile, ICommandListener commandListener)
        {
            _rumination = rumination;
            _description = rumination.Description;
            _rumination.RuminationChanged += OnRuminationChanged;

            UpdateRuminationCommand = new UpdateRuminationCommand(this);
            UpdateRuminationDescriptionCommand = new UpdateRuminationDescriptionCommand(_rumination, pile, commandListener);
        }

        public void CheckRumination()
        {
            IsChecked = true;
        }

        public void UncheckRumination()
        {
            IsChecked = false;
        }

        private void OnRuminationChanged(Rumination rumination)
        {
            Description = rumination.Description;
        }
    }
}
