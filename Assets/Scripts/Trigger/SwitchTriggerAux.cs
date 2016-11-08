using UnityEngine;
using System.Collections;

public abstract class SwitchTriggerAux : MonoBehaviour
{
    private bool _activated = false;
    public abstract bool Activated
    {
        get;
        set;
    }
}
