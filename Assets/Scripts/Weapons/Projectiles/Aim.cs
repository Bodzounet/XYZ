using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    void Awake()
    {
        RaycastHit hit;
        Transform t = Camera.main.transform;

        if (Physics.Raycast(t.transform.position, t.forward, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Wall")))
            transform.LookAt(hit.point);
        else
            transform.LookAt(Camera.main.transform.forward * 50000000);
    }
}
