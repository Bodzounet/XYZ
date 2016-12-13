using UnityEngine;
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

    [SerializeField]
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

    protected bool _isAltShooting = false;
    protected bool IsAltShooting
    {
        get { return _isAltShooting; }
        set
        {
            _isAltShooting = value;
            _anim.SetBool("AltShoot", value);
        }
    }

    protected bool _isCharging = false;
    protected bool IsCharging
    {
        get { return _isCharging; }
        set
        {
            _isCharging = value;
            _anim.SetBool("Charge", value);
        }
    }

    public float mainProjectileVelocity;
    public float altProjectileVelocity;

    public int mainProjectileAmmoConsumption;
    public int altProjectileAmmoConsumption;

    void Awake()
    {
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
        IsAltShooting = false;
        _clip.ReloadClip();
    }

    public void Anim_ShootEnd()
    {
        Debug.Log("caca");

        IsShooting = false;
        IsAltShooting = false;
    }

    public void Anim_OutOfAmmoEnd()
    {
        IsShooting = false;
        IsAltShooting = false;
        IsReloading = false;
    }

    public virtual void Reload()
    {
        if (IsCharging || IsShooting || IsAltShooting)
            return;

        IsReloading = true;
    }

    public virtual void MainShoot()
    {
        if (IsShooting || IsAltShooting || IsReloading)
            return;

        if (_clip.AmmoRemaining >= mainProjectileAmmoConsumption)
            IsShooting = true;
        else
            IsReloading = true;
    }

    public virtual void AltShoot()
    {
        if (IsShooting || IsAltShooting || IsReloading)
            return;

        if (_clip.AmmoRemaining >= altProjectileAmmoConsumption)
            IsAltShooting = true;
        else
            IsReloading = true;
    }

    public virtual void EndChargeMainShoot() { } // when releasing left click, not needed for each weapon.
    public virtual void EndChargeAltShoot() { }

    public virtual void SwitchWeapon()
    {
        _anim.SetInteger("InventoryAmmo", _inventory.ammos[_clip.ammoType].StockAmmo);
        _anim.SetInteger("ClipAmmo", _clip.AmmoRemaining);
        IsReloading = false;
        IsShooting = false;
        IsAltShooting = false;
        IsCharging = false;
        _anim.Play("Idle");
    }

    public void Anim_MainShoot()
    {
        _Core_MainShoot();
    }

    public void Anim_AltShoot()
    {
        _Core_AltShoot();
    }

    public void Anim_Idle()
    {
        _anim.SetInteger("InventoryAmmo", _inventory.ammos[_clip.ammoType].StockAmmo);
        _anim.SetInteger("ClipAmmo", _clip.AmmoRemaining);
        IsReloading = false;
        IsShooting = false;
        IsAltShooting = false;
        IsCharging = false;

        if (_clip.AmmoRemaining < mainProjectileAmmoConsumption && _clip.AmmoRemaining < altProjectileAmmoConsumption && 
            (_inventory.ammos[_clip.ammoType].StockAmmo > mainProjectileAmmoConsumption || _inventory.ammos[_clip.ammoType].StockAmmo > altProjectileAmmoConsumption))
            IsReloading = true;
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
