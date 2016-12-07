using UnityEngine;
using System.Collections.Generic;

public class SingleShotDamage : MonoBehaviour
{
    public int damages;
    public bool multiTarget;
    public bool destroyOnHit;

    private List<GameObject> hit = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Target" && !hit.Contains(col.gameObject) && !(!multiTarget && hit.Count > 0))
        {
            col.GetComponentInParent<EnemyHealthManager>().TakeDamages(damages);
            hit.Add(col.gameObject);

            if (destroyOnHit)
                Destroy(this.gameObject);
        }
    }
}
