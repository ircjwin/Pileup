﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Piles.AttachedProperties
{
    public static class ListViewDragDropBehavior
    {
        public static readonly DependencyProperty EnableDragDropProperty =
            DependencyProperty.RegisterAttached(
                "EnableDragDrop",
                typeof(bool),
                typeof(ListViewDragDropBehavior),
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
            if (d is ListView listView)
            {
                bool isEnabled = (bool)e.NewValue;

                if (isEnabled)
                {
                    listView.PreviewMouseLeftButtonDown += ListView_PreviewMouseLeftButtonDown;
                    listView.PreviewMouseMove += ListView_PreviewMouseMove;
                    listView.PreviewMouseLeftButtonUp += ListView_PreviewMouseLeftButtonUp;
                    listView.DragOver += ListView_DragOver;
                    listView.Drop += ListView_Drop;
                }
                else
                {
                    listView.PreviewMouseLeftButtonDown -= ListView_PreviewMouseLeftButtonDown;
                    listView.PreviewMouseMove -= ListView_PreviewMouseMove;
                    listView.PreviewMouseLeftButtonUp -= ListView_PreviewMouseLeftButtonUp;
                    listView.DragOver -= ListView_DragOver;
                    listView.Drop -= ListView_Drop;
                }
            }
        }

        private static Point _startPoint;
        private static bool _isDragging = false;
        private static object _draggedItem;
        private static ListView _sourceListView;
        private static readonly string DraggedChildDataFormat = "DraggedChild";

        private static void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView)
            {
                _startPoint = e.GetPosition(null);

                var item = FindVisualParent<ListViewItem>((DependencyObject)e.OriginalSource);
                if (item != null)
                {
                    _draggedItem = item.DataContext;
                    _sourceListView = listView;
                }
            }
        }

        private static void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging && _draggedItem != null)
            {
                Point position = e.GetPosition(null);
                Vector diff = _startPoint - position;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isDragging = true;

                    DataObject dragData = new DataObject(DraggedChildDataFormat, _draggedItem);
                    DragDrop.DoDragDrop(_sourceListView, dragData, DragDropEffects.Move);
                }
            }
        }

        private static void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            _draggedItem = null;
            _sourceListView = null;
        }

        private static void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DraggedChildDataFormat))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private static void ListView_Drop(object sender, DragEventArgs e)
        {
            if (_isDragging && e.Data.GetDataPresent(DraggedChildDataFormat))
            {
                var listView = sender as ListView;
                if (listView == null) return;

                object draggedItem = e.Data.GetData(DraggedChildDataFormat);
                IList itemsSource = listView.ItemsSource as IList;
                if (itemsSource == null) return;

                Point dropPoint = e.GetPosition(listView);
                var targetListViewItem = FindListViewItemAtPoint(listView, dropPoint);

                int sourceIndex = itemsSource.IndexOf(draggedItem);
                int targetIndex;

                if (targetListViewItem != null)
                {
                    object targetItem = targetListViewItem.DataContext;
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
            _sourceListView = null;
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

        private static ListViewItem FindListViewItemAtPoint(ListView listView, Point point)
        {
            HitTestResult result = VisualTreeHelper.HitTest(listView, point);
            if (result == null) return null;

            return FindVisualParent<ListViewItem>(result.VisualHit);
        }
    }
}
