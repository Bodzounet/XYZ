using UnityEngine;
using System.Collections;

public class InitialImpulsion : MonoBehaviour
{
    public float intensity;
    public GameObject explosionEffect;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * intensity);
        lastPosition = transform.position;
    }

    Vector3 lastPosition;
    void Update()
    {
        var direction = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;
        if (direction != Vector3.zero)
            transform.forward = direction;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall") || col.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            Destroy(this.gameObject);

            var v = GameObject.Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(v, 2.5f);
        }
    }
}
