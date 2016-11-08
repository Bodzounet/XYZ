using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// this script hold the trigger.
/// when the player triggers it, it will find the script (Atrigger) holding the behaviour of the trigger.
/// you should have place the Atrigger as a child of TriggerReferences.
/// 
/// this is done so that trigger as easily savable.
/// only the triggerManager has to be save.
/// 
/// work only with box and sphere colliders;
/// </summary>
public class TriggerManager : MonoBehaviour
{
    public int triggerId;
    public bool isSwitch;
    public bool isActivated;

    private void _SwitchActivated()
    {
        isActivated = !isActivated;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Avatar")
        {
            if (!isSwitch)
            {
                FindObjectOfType<TriggerReferences>().getTriggerById(triggerId).Activate();
                Destroy(this.gameObject);
            }
            else
            {
                var v = col.GetComponent<AvatarController>();
                v.SwitchTrigger += FindObjectOfType<TriggerReferences>().getTriggerById(triggerId).Activate;
                v.SwitchTrigger += _SwitchActivated;
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Avatar")
        {
            if (isSwitch)
            {
                var v = col.GetComponent<AvatarController>();
                v.SwitchTrigger -= FindObjectOfType<TriggerReferences>().getTriggerById(triggerId).Activate;
                v.SwitchTrigger -= _SwitchActivated;
            }
        }
    }
}
