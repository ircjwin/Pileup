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

        public static readonly DependencyProperty DragDropItemTemplateProperty =
            DependencyProperty.RegisterAttached(
                "DragDropItemTemplate",
                typeof(DataTemplate),
                typeof(TabControlDragDropBehavior),
                new PropertyMetadata(null));

        public static DataTemplate GetDragDropItemTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DragDropItemTemplateProperty);
        }

        public static void SetDragDropItemTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DragDropItemTemplateProperty, value);
        }

        private static Point _startPoint;
        private static bool _isDragging = false;
        private static object _draggedItem;
        private static TabControl _sourceTabControl;
        private static readonly string DraggedDataFormat = "DraggedItem";

        private static void TabControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                _startPoint = e.GetPosition(null);

                // Try to find the TabItem being clicked
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

                // Check if the mouse has moved far enough to start a drag operation
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isDragging = true;

                    // Create data object for dragging
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

                // Get the position where the item is dropped
                Point dropPoint = e.GetPosition(tabControl);
                var targetTabItem = FindTabItemAtPoint(tabControl, dropPoint);

                int sourceIndex = itemsSource.IndexOf(draggedItem);
                int targetIndex;

                if (targetTabItem != null)
                {
                    // Get the target item (the one we're dropping onto)
                    object targetItem = targetTabItem.DataContext;
                    targetIndex = itemsSource.IndexOf(targetItem);

                    // If we're dropping on ourselves, do nothing
                    if (targetIndex == sourceIndex)
                        return;

                    // If the target is after the source, we need to adjust the index
                    // because removing the source item will shift everything down
                    if (targetIndex > sourceIndex)
                        targetIndex--;
                }
                else
                {
                    // If dropped in empty space, move to the end
                    targetIndex = itemsSource.Count - 1;
                }

                // Move the item in the collection
                itemsSource.Remove(draggedItem);

                // Insert at the target position or at the end if targetIndex is invalid
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
