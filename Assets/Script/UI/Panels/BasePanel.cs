using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel
{
    public string path { get; protected set; }
    public virtual void OnEnter() { }
    public BasePanel(string path)
    {
        this.path = path;
    }
    public virtual void OnPause(){ }
    public virtual void OnResume() { }
    public virtual void OnExit() { }
}
