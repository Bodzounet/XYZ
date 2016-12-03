using UnityEngine;
using System.Collections;

namespace AI
{
    public delegate void PlayerTrigger(Transform player);

    [RequireComponent(typeof(SphereCollider))]
    public class LoseAggro : MonoBehaviour
    {
        public event PlayerTrigger OnLoseAggro;

        public void OnTriggerExit(Collider col)
        {
            if (col.tag == "Avatar")
            {
                if (OnLoseAggro != null)
                    OnLoseAggro(col.transform);
            }
        }
    }
}