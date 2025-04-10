using Piles.Commands;
using Piles.Models;
using System.Windows.Input;

namespace Piles.ViewModels
{
    public class RuminationViewModel : ViewModelBase
    {
        private bool _isDescriptionReadOnly = true;
        public bool IsDescriptionReadOnly
        {
            get { return _isDescriptionReadOnly; }
            set
            {
                _isDescriptionReadOnly = value;
                OnPropertyChanged();
            }
        }

        private bool _isDescriptionHittable = false;
        public bool IsDescriptionHittable
        {
            get { return _isDescriptionHittable; }
            set
            {
                _isDescriptionHittable = value;
                OnPropertyChanged();
            }
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

        public ICommand UpdateRuminationCommand { get; }
        public ICommand UpdateRuminationDescriptionCommand { get; }

        public RuminationViewModel(Rumination rumination)
        {
            _rumination = rumination;
            _description = rumination.Description;

            UpdateRuminationCommand = new UpdateRuminationCommand(this);
            UpdateRuminationDescriptionCommand = new UpdateRuminationDescriptionCommand(_rumination);
        }

        public void CheckRumination()
        {
            IsChecked = true;
        }

        public void UncheckRumination()
        {
            IsChecked = false;
        }
    }
}
