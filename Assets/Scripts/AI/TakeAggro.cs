using UnityEngine;
using System.Collections;

namespace AI
{
    [RequireComponent(typeof(SphereCollider))]
    public class TakeAggro : MonoBehaviour
    {
        public event PlayerTrigger OnTakeAggro;

        void OnTriggerStay(Collider col)
        {
            if (col.tag == "Avatar")
            {
                if (Physics.Linecast(transform.position, col.transform.position, 1 << LayerMask.NameToLayer("Wall"))) // if the player is in another room, ignore it.
                    return;

                if (OnTakeAggro != null)
                    OnTakeAggro(col.transform);
            }
        }
    }
}