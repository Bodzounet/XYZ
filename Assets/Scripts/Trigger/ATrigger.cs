using UnityEngine;
using System.Collections;

public abstract class ATrigger : MonoBehaviour
{
    public int id;

    public abstract void Activate();
}
