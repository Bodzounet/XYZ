using UnityEngine;
using System.Collections;
using System;

public class PlasmaGun : AWeapon
{
    protected override void _Core_AltShoot()
    {
        Instantiate(altProjectile, _projectileSpawnPos.position, _projectileSpawnPos.rotation);

        _clip.AmmoRemaining -= altProjectileAmmoConsumption;
    }

    protected override void _Core_MainShoot()
    {
        GameObject proj = Instantiate(mainProjectile, _projectileSpawnPos.position, _projectileSpawnPos.rotation) as GameObject;
        Destroy(proj, 1);

        _clip.AmmoRemaining -= mainProjectileAmmoConsumption;
    }
}
