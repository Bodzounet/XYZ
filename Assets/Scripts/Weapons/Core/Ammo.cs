using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ammo
{
    public delegate void StockAmmoChange(int newStock, int maxCapacity);
    public event StockAmmoChange OnStockAmmoChanged;

    public enum e_AmmoType
    {
        Red,
        Blue
    }

    [SerializeField]
    private int _maxAmmo;
    public int MaxAmmo
    {
        get { return _maxAmmo; }
        set
        {
            _maxAmmo = value;
        }
    }

    private int _ammo;
    public int StockAmmo
    {
        get { return _ammo; }
        set
        {
            _ammo = Mathf.Clamp(value, 0, MaxAmmo);
            if (OnStockAmmoChanged != null)
                OnStockAmmoChanged(_ammo, _maxAmmo);
        }
    }
}
