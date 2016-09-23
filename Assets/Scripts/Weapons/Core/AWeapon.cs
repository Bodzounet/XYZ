﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Clip))]
public abstract class AWeapon : MonoBehaviour
{
    public GameObject mainProjectile;
    public GameObject altProjectile;

    protected Clip _clip;
    public Clip Clip
    {
        get { return _clip; }
    }

    protected Transform _projectileSpawnPos;
    protected Animator _anim;

    protected Inventory _inventory;

    protected bool _isReloading = false;
    protected bool IsReloading
    {
        get { return _isReloading; }
        set
        {
            _isReloading = value;
            _anim.SetBool("Reload", value);
        }
    }

    protected bool _isShooting = false;
    protected bool IsShooting
    {
        get { return _isShooting; }
        set
        {
            _isShooting = value;
            _anim.SetBool("Shoot", value);
        }
    }

    public float mainProjectileVelocity;
    public float altProjectileVelocity;

    void Awake()
    {
        _projectileSpawnPos = transform.FindChild("ProjectileSpawnPos");
        _clip = GetComponent<Clip>();
        _anim = GetComponent<Animator>();
        _inventory = GetComponentInParent<Inventory>();
    }

    void Start()
    {
        _anim.SetInteger("Ammos", _inventory.ammos[_clip.ammoType].StockAmmo);
        Reload();
    }

    public void Anim_ReloadEnd()
    {
        IsReloading = false;
        _clip.ReloadClip();
    }

    public void Anim_ShootEnd()
    {
        IsShooting = false;
        if (_clip.AmmoRemaining == 0)
            Reload();
    }

    public void Anim_OutOfAmmoEnd()
    {
        IsShooting = false;
        IsReloading = false;
    }

    public virtual void Reload()
    {
        _anim.SetInteger("Ammos", _inventory.ammos[_clip.ammoType].StockAmmo);
        IsReloading = true;
    }

    public virtual void MainShoot()
    {
        if (IsShooting || IsReloading)
            return;

        _anim.SetInteger("Ammos", _clip.AmmoRemaining);
        IsShooting = true;

        if (_clip.AmmoRemaining != 0)
        {
            _Core_MainShoot();
        }
    }

    public virtual void AltShoot()
    {
        if (IsShooting || IsReloading)
            return;

        _anim.SetInteger("Ammos", _clip.AmmoRemaining);
        IsShooting = true;

        if (_clip.AmmoRemaining != 0)
        {
            _Core_AltShoot();
        }
    }

    protected abstract void _Core_MainShoot();
    protected abstract void _Core_AltShoot();
}