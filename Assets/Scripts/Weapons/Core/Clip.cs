using UnityEngine;
using System.Collections;

public class Clip : MonoBehaviour
{
    public delegate void StockClipChange(int newStock, int maxCapacity);
    public event StockClipChange OnStockClipChanged;

    public Ammo.e_AmmoType ammoType;

    private Inventory _inventory;
    public Inventory Inventory
    {
        get { return _inventory; }
    }

    [SerializeField]
    private int _maxCapacity;
    public int MaxCapacity
    {
        get { return _maxCapacity; }
        set
        {
            _maxCapacity = value;
        }
    }

    private int _ammoRemaining;
    public int AmmoRemaining
    {
        get { return _ammoRemaining; }
        set
        {
            _ammoRemaining = value;
            if (OnStockClipChanged != null)
                OnStockClipChanged(value, MaxCapacity);
        }
    }

    void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
    }

    public bool ReloadClip()
    {
        int ammo = Mathf.Min(MaxCapacity - AmmoRemaining, Inventory.ammos[ammoType].StockAmmo);
        if (ammo == 0)
            return false;
        Inventory.ammos[ammoType].StockAmmo -= ammo;
        AmmoRemaining += ammo;
        return true;
    }
}
