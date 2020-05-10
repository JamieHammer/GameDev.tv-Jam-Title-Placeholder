using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To help with updating the stack on changes.
/// </summary>

public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public ObservableStack(ObservableStack<T> items) : base(items)
    {
        
    }

    public ObservableStack()
    {

    }

    public new void Push(T item)
    {
        base.Push(item);

        OnPush?.Invoke();
    }

    public new T Pop()
    {
        T item = base.Pop();

        OnPop?.Invoke();

        return item;
    }

    public new void Clear()
    {
        base.Clear();

        OnClear?.Invoke();
    }
}
