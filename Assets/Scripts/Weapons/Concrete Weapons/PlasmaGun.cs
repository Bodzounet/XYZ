using UnityEngine;
using System.Collections;
using System;

public class PlasmaGun : AWeapon
{
    protected override void _Core_AltShoot()
    {
        throw new NotImplementedException();
    }

    protected override void _Core_MainShoot()
    {
        GameObject proj = Instantiate(mainProjectile, _projectileSpawnPos.position, _projectileSpawnPos.rotation) as GameObject;
        Destroy(proj, 1);
    }
}
