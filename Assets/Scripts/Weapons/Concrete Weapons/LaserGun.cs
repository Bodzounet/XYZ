using UnityEngine;
using System.Collections;
using System;

public class LaserGun : AWeapon
{
    public delegate void ChargeLevelChanged(int chargeIntensity);
    public event ChargeLevelChanged OnChargeLevelChanged;

    private int _charge = 0;
    private Coroutine _routine = null;
    public int Charge
    {
        get { return _charge; }
        set
        {
            _charge = value;
            if (OnChargeLevelChanged != null)
                OnChargeLevelChanged(value);
        }
    }

    protected override void _Core_MainShoot()
    {
        Debug.Log("cc");
        GameObject proj = Instantiate(mainProjectile, _projectileSpawnPos.position, Quaternion.identity) as GameObject;
        proj.GetComponent<ModulateLaserGunProjectileSize>().SetSize(Charge);
        proj.GetComponent<SingleShotDamage>().damages += Charge * 3;
        proj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, mainProjectileVelocity));
        Charge = 0;
    }

    protected override void _Core_AltShoot()
    {
        IsShooting = false;
    }

    public override void MainShoot()
    {
        if (IsShooting || IsAltShooting || IsReloading)
            return;

        if (_clip.AmmoRemaining >= altProjectileAmmoConsumption)
        {
            IsShooting = true;
            IsCharging = true;
            _routine = StartCoroutine(Co_ChargeShoot());
        }
        else
            IsReloading = true;
    }

    public override void EndChargeMainShoot()
    {
        if (!IsCharging)
            return;

        IsCharging = false;

        if (_routine != null)
            StopCoroutine(_routine);
    }

    private IEnumerator Co_ChargeShoot()
    {
        while (_clip.AmmoRemaining > 0)
        {
            Charge++;
            _clip.AmmoRemaining--;
            yield return new WaitForSeconds(0.5f);
        }

        IsCharging = false;
    }
}
