using UnityEngine;
using System.Collections;

namespace AI
{
    public class CleanExplosion : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Co_Clean());
        }

        private IEnumerator Co_Clean()
        {
            Vector3 localScale = transform.localScale;
            for (int i = 0; i < 50; i++)
            {
                transform.localScale = localScale * (1 - 0.01f * i);

                yield return new WaitForSeconds(0.08f);
            }
            Destroy(transform.GetComponent<Collider>());
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 150);
            Destroy(this.gameObject, 2);
        }
    }
}