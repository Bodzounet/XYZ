using UnityEngine;
using System.Collections;
using System;

public class LaserGun : AWeapon
{
    protected override void _Core_AltShoot()
    {
        IsShooting = false;
    }

    protected override void _Core_MainShoot()
    {
        GameObject proj = Instantiate(mainProjectile, _projectileSpawnPos.position, Quaternion.identity) as GameObject;
        proj.transform.forward = _projectileSpawnPos.forward;
        proj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, mainProjectileVelocity));

        _clip.AmmoRemaining -= mainProjectileAmmoConsumption;
    }
}
