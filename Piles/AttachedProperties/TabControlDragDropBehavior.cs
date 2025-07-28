using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Piles.AttachedProperties
{
    class TabControlDragDropBehavior
    {
        public static readonly DependencyProperty EnableDragDropProperty =
            DependencyProperty.RegisterAttached(
                "EnableDragDrop",
                typeof(bool),
                typeof(TabControlDragDropBehavior),
                new PropertyMetadata(false, OnEnableDragDropChanged));

        public static bool GetEnableDragDrop(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableDragDropProperty);
        }

        public static void SetEnableDragDrop(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableDragDropProperty, value);
        }

        private static void OnEnableDragDropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl)
            {
                bool isEnabled = (bool)e.NewValue;

                if (isEnabled)
                {
                    tabControl.PreviewMouseLeftButtonDown += TabControl_PreviewMouseLeftButtonDown;
                    tabControl.PreviewMouseMove += TabControl_PreviewMouseMove;
                    tabControl.PreviewMouseLeftButtonUp += TabControl_PreviewMouseLeftButtonUp;
                    tabControl.DragOver += TabControl_DragOver;
                    tabControl.Drop += TabControl_Drop;
                }
                else
                {
                    tabControl.PreviewMouseLeftButtonDown -= TabControl_PreviewMouseLeftButtonDown;
                    tabControl.PreviewMouseMove -= TabControl_PreviewMouseMove;
                    tabControl.PreviewMouseLeftButtonUp -= TabControl_PreviewMouseLeftButtonUp;
                    tabControl.DragOver -= TabControl_DragOver;
                    tabControl.Drop -= TabControl_Drop;
                }
            }
        }

        private static Point _startPoint;
        private static bool _isDragging = false;
        private static object _draggedItem;
        private static TabControl _sourceTabControl;
        private static readonly string DraggedDataFormat = "DraggedItem";
        private static readonly string DraggedChildDataFormat = "DraggedChild";

        private static void TabControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                _startPoint = e.GetPosition(null);

                var item = FindVisualParent<TabItem>((DependencyObject)e.OriginalSource);
                if (item != null)
                {
                    _draggedItem = item.DataContext;
                    _sourceTabControl = tabControl;
                }
            }
        }

        private static void TabControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging && _draggedItem != null)
            {
                Point position = e.GetPosition(null);
                Vector diff = _startPoint - position;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isDragging = true;

                    DataObject dragData = new DataObject(DraggedDataFormat, _draggedItem);
                    DragDrop.DoDragDrop(_sourceTabControl, dragData, DragDropEffects.Move);
                }
            }
        }

        private static void TabControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            _draggedItem = null;
            _sourceTabControl = null;
        }

        private static void TabControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DraggedDataFormat))
            {
                e.Effects = DragDropEffects.Move;
            }
            else if (e.Data.GetDataPresent(DraggedChildDataFormat))
            {
                var tabControl = sender as TabControl;
                if (tabControl == null) return;

                IList itemsSource = tabControl.ItemsSource as IList;
                Point dropPoint = e.GetPosition(tabControl);
                var targetTabItem = FindTabItemAtPoint(tabControl, dropPoint);
                if (targetTabItem == null) return;

                object targetItem = targetTabItem.DataContext;
                int targetIndex = itemsSource.IndexOf(targetItem);
                tabControl.SelectedIndex = targetIndex;
                //e.Effects = DragDropEffects.None;
                return;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private static void TabControl_Drop(object sender, DragEventArgs e)
        {
            if (_isDragging && e.Data.GetDataPresent(DraggedDataFormat))
            {
                var tabControl = sender as TabControl;
                if (tabControl == null) return;

                object draggedItem = e.Data.GetData(DraggedDataFormat);
                IList itemsSource = tabControl.ItemsSource as IList;
                if (itemsSource == null) return;

                Point dropPoint = e.GetPosition(tabControl);
                var targetTabItem = FindTabItemAtPoint(tabControl, dropPoint);

                int sourceIndex = itemsSource.IndexOf(draggedItem);
                int targetIndex;

                if (targetTabItem != null)
                {
                    object targetItem = targetTabItem.DataContext;
                    targetIndex = itemsSource.IndexOf(targetItem);

                    if (targetIndex == sourceIndex)
                        return;

                    if (targetIndex > sourceIndex)
                        targetIndex--;
                }
                else
                {
                    targetIndex = itemsSource.Count - 1;
                }

                itemsSource.Remove(draggedItem);

                if (targetIndex >= 0 && targetIndex < itemsSource.Count)
                    itemsSource.Insert(targetIndex, draggedItem);
                else
                    itemsSource.Add(draggedItem);
            }

            _isDragging = false;
            _draggedItem = null;
            _sourceTabControl = null;
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null) return null;

            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;

            return FindVisualParent<T>(parentObject);
        }

        private static TabItem FindTabItemAtPoint(TabControl tabControl, Point point)
        {
            HitTestResult result = VisualTreeHelper.HitTest(tabControl, point);
            if (result == null) return null;

            return FindVisualParent<TabItem>(result.VisualHit);
        }
    }
}
