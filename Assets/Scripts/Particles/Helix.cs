using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Helix : MonoBehaviour
{
    [Range(2, 100)]
    public int resolution; // number of point in the curve;
    public int frequency; // number of spires
    public float amplitude;

    public float ZVelocity;

    public bool doit = false;

    void Update()
    {
        if (doit)
        {
            CreateHelix();
            doit = false;
        }
    }

    void CreateHelix()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.startSpeed = 0;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;

        velocity.z = new ParticleSystem.MinMaxCurve(10.0f, ZVelocity);

        AnimationCurve XCurve = new AnimationCurve();
        AnimationCurve YCurve = new AnimationCurve();

        float newAmplitude = amplitude;

        for (int i = 0; i < resolution; i++)
        {
            float t = (float)i / (resolution - 1);
            XCurve.AddKey(t, newAmplitude * Mathf.Cos(t * 2 * frequency * Mathf.PI));
            YCurve.AddKey(t, newAmplitude * Mathf.Sin(t * 2 * frequency * Mathf.PI));

            newAmplitude = Mathf.Lerp(amplitude, 0, (float)i / resolution);
        }

        velocity.x = new ParticleSystem.MinMaxCurve(10.0f, XCurve);
        velocity.y = new ParticleSystem.MinMaxCurve(10.0f, YCurve);
    }
}
