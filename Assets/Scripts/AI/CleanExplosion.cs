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
            //var renderer = GetComponent<Renderer>();
            for (int i = 0; i < 50; i++)
            {
                //foreach (var v in renderer.materials)
                //{
                //    var newColor = v.color;
                //    newColor.a = 0;
                //    v.color = Color.Lerp(v.color, newColor, 0.02f * i);
                //    yield return new WaitForSeconds(0.1f);
                //}
                transform.localScale = Vector3.one * (1 - 0.01f * i);

                yield return new WaitForSeconds(0.08f);
            }
            Destroy(transform.GetComponent<Collider>());
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 150);
            Destroy(this.gameObject, 2);
        }
    }
}