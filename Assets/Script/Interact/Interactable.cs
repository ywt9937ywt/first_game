using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable: MonoBehaviour
{
    public UnityEvent onUserHover = new UnityEvent();
    public UnityEvent onUserLeave = new UnityEvent();
    public UnityEvent onSelected = new UnityEvent();
    public UnityEvent offSelected = new UnityEvent();
    public void Awake()
    {

    }
    public void TriggeronUserHover()
    {
        onUserHover.Invoke();
    }

    public void TriggeronUserLeave()
    {
        onUserLeave.Invoke();
    }

    public void TriggerOnSelected()
    {
        onSelected.Invoke();
    }

    public void TriggerOffSelected()
    {
        offSelected.Invoke();
    }
}
