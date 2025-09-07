using System;
using UniRx;
using UnityEngine;

public abstract class ViewModel : IDisposable
{
    protected readonly CompositeDisposable disposables = new();

    public virtual void Initialize(){}

    public void Dispose()
    {
        disposables.Dispose();
    }
}
