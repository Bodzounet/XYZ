using UnityEngine;
using System.Collections;

public class LerpMaterialAnim : MonoBehaviour
{
    private Material _start;
    private Material _end;

    private int _materialIdx;

    Material mat;

    public Material StartMat
    {
        get { return _start; }
        set
        {
            _start = value;
        }
    }

    public Material EndMat
    {
        get { return _end; }
        set
        {
            _end = value;
        }
    }

    public int MaterialIdx
    {
        get { return _materialIdx; }
        set
        {
            _materialIdx = value;
        }
    }

    void Start()
    {
        var mr = GetComponentInChildren<MeshRenderer>();
        mat = mr.materials[MaterialIdx];
    }

    public void Lerp(float i)
    {
        mat.Lerp(StartMat, EndMat, i);
    }
}
