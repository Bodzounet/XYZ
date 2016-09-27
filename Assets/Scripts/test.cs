using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public Material start;
    public Material end;

    Material mat;

    void Start()
    {
        var mr = GetComponent<MeshRenderer>();
        mat = mr.materials[0];
    }

    public void LerpUp(float i)
    {
        mat.Lerp(start, end, i);
    }
}
