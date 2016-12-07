using UnityEngine;
using System.Collections;

public class PlasmaGun_MainShot : MonoBehaviour
{
    public float intensity;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * intensity);
    }
}
