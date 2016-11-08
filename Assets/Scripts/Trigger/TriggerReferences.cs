using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// for convinience, put all Atrigger as children of this GO
/// </summary>
public class TriggerReferences : MonoBehaviour
{
    public ATrigger[] triggers;
    
    public ATrigger getTriggerById(int id)
    {
        return triggers.Single(x => x.id == id);
    }
}
