using System;
using System.Collections.Generic;

namespace Piles.Core
{
    public class ObservableStack<T> : Stack<T>
    {
        public event Action StackChanged;

        public void ObservablePush(T item)
        {
            Push(item);
            OnStackChanged();
        }

        public T ObservablePop()
        {
            T item = Pop();
            OnStackChanged();
            return item;
        }

        public void OnStackChanged()
        {
            StackChanged?.Invoke();
        }
    }
}
