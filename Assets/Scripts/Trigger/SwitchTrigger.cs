using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// switch are binary : on / off only.
/// a switch can activate one or multiple SwitchTriggerAux.
/// place the auxScript on a GO and implement your behaviour in it.
/// </summary>
public class SwitchTrigger : ATrigger
{
    public delegate void ActivateSwitchTrigger();

    public List<SwitchTriggerAux> activators;

    public override void Activate()
    {
        foreach (var v in activators)
        {
            v.Activated = !v.Activated;
        }
    }
}
