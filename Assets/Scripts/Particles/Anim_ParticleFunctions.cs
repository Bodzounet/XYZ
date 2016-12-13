using UnityEngine;
using System.Collections;

public class Anim_ParticleFunctions : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] ps;

    public void Anim_Play()
    {
        foreach (var v in ps)
        {
            v.Play();
        }
    }
}
