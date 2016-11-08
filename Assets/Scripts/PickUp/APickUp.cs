using UnityEngine;
using System.Collections;

public abstract class APickUp : MonoBehaviour
{
    public enum e_PickUpId
    {
        Red, // red ammo
        Blue, // blue ammo
        Green, // life
        Yellow // armor
    }

    public e_PickUpId id;

    public abstract void OnTriggerEnter(Collider col);
}
