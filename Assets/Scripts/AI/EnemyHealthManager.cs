using UnityEngine;
using System.Collections;
using System;

public class EnemyHealthManager : HealthManager
{
    public delegate void Die();
    public event Die OnDie;

    public override void TakeDamages(int damages)
    {
        Life -= damages;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamages(10);
        }
    }

    protected override void _Die()
    {
        if (OnDie != null)
            OnDie();
    }
}
