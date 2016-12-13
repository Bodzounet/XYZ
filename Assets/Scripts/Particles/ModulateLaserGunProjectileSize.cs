using UnityEngine;
using System.Collections;

public class ModulateLaserGunProjectileSize : MonoBehaviour
{
    public void SetSize(int charge)
    {
        var v = GetComponentInChildren<ParticleSystem>();
        v.Stop();
        v.startSize = 0.05f * (1 + charge);
        v.Play();
    }
}
