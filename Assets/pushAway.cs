using UnityEngine;
using System.Collections;

public class pushAway : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddExplosionForce(Random.Range(500, 1000), transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)), Random.Range(10, 20));
    }
}
