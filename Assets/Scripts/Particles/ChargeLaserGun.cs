using UnityEngine;
using System.Collections;

public class ChargeLaserGun : MonoBehaviour
{
    ParticleSystem _ps;

    void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
        GetComponentInParent<LaserGun>().OnChargeLevelChanged += CB_SetIntensity;
    }

    void CB_SetIntensity(int intensity)
    {
        var emission = _ps.emission;
        var rate = new ParticleSystem.MinMaxCurve(50 * intensity);
        emission.rate = rate;
    }
}
