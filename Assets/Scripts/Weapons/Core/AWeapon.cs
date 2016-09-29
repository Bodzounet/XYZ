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

    public int mainProjectileAmmoConsumption;
    public int altProjectileAmmoConsumption;

    void Awake()
    {
        _projectileSpawnPos = transform.FindChild("ProjectileSpawnPos");
        _clip = GetComponent<Clip>();
        _anim = GetComponent<Animator>();
        _inventory = GetComponentInParent<Inventory>();

        _clip.OnStockClipChanged += CB_OnClipAmmoChange;
        _inventory.ammos[_clip.ammoType].OnStockAmmoChanged += CB_OnInventoryAmmoChange;
    }

    void Start()
    {
        SwitchWeapon();
    }

    public void Anim_ReloadEnd()
    {
        IsReloading = false;
        IsShooting = false;
        _clip.ReloadClip();
    }

    public void Anim_ShootEnd()
    {
        IsShooting = false;
    }

    public void Anim_OutOfAmmoEnd()
    {
        IsShooting = false;
        IsReloading = false;
    }

    public virtual void Reload()
    {
        IsReloading = true;
    }

    public virtual void MainShoot()
    {
        if (IsShooting || IsReloading)
            return;

        if (_clip.AmmoRemaining >= mainProjectileAmmoConsumption)
            IsShooting = true;
        else
            IsReloading = true;
    }

    public virtual void AltShoot()
    {
        if (IsShooting || IsReloading)
            return;

        IsShooting = true;
    }

    public virtual void SwitchWeapon()
    {
        _anim.SetInteger("InventoryAmmo", _inventory.ammos[_clip.ammoType].StockAmmo);
        _anim.SetInteger("ClipAmmo", _clip.AmmoRemaining);
        IsReloading = false;
        IsShooting = false;
        _anim.Play("Idle");
    }

    public void Anim_MainShoot()
    {
        if (_clip.AmmoRemaining >= mainProjectileAmmoConsumption)
        {
            _Core_MainShoot();
        }
    }

    public void Anim_AltShoot()
    {
        if (_clip.AmmoRemaining != 0)
        {
            _Core_AltShoot();
        }
    }

    protected abstract void _Core_MainShoot();
    protected abstract void _Core_AltShoot();

    private void CB_OnClipAmmoChange(int newStock, int maxCapacity)
    {
        _anim.SetInteger("ClipAmmo", newStock);
    }

    private void CB_OnInventoryAmmoChange(int newStock, int maxCapacity)
    {
        _anim.SetInteger("InventoryAmmo", newStock);
    }
}
