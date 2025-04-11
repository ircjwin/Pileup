using System.Windows;
using System.Windows.Controls;

namespace Piles.AttachedProperties
{
    public static class FocusExtension
    {
        public static readonly DependencyProperty IsFocusedProperty = 
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(FocusExtension), new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = d as TextBox;
            if ((bool)e.NewValue)
            {
                uie.Focus();
                uie.SelectAll();
            }
        }
    }
}
