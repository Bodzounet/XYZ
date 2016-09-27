﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeaponController : MonoBehaviour {

    public delegate void WeaponChanged(AWeapon oldWeapon, AWeapon newWeapon);
    public event WeaponChanged OnWeaponChanged;

    private List<WeaponData> _weapons = new List<WeaponData>();

    private AWeapon _currentWeapon;
    public AWeapon CurrentWeapon
    {
        get { return _currentWeapon; }
        set
        {
            var old = _currentWeapon;
            _currentWeapon = value;

            if (old != null)
            {
                _weapons.Single(x => x.weapon == old).selected = false;
                old.gameObject.SetActive(false);
            }
            _weapons.Single(x => x.weapon == value).selected = true;
            value.gameObject.SetActive(true);

            Debug.Log(value.gameObject.name);

            if (OnWeaponChanged != null)
                OnWeaponChanged(old, value);
        }
    }

    void Awake()
    {
        foreach (var weapon in GetComponentsInChildren<AWeapon>(true))
        {
            _weapons.Add(new WeaponData(weapon, false));
        }
        _weapons.OrderBy(x => x.weapon.gameObject.transform.GetSiblingIndex());

        _weapons[1].unlocked = true;
    }

    void Start()
    {
        CurrentWeapon = _weapons[1].weapon;
    }

    void Update ()
    {
        _MainShoot();
        _AltShoot();
        _Reload();
        _SwitchWeapon();
    }

    private void _MainShoot()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            CurrentWeapon.MainShoot();
        }
    }

    private void _AltShoot()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            CurrentWeapon.AltShoot();
        }
    }

    private void _Reload()
    {
        if (Input.GetAxis("Reload") > 0)
        {
            CurrentWeapon.Reload();
        }     
    }

    private void _SwitchWeapon()
    {
        WeaponData wp = _weapons.Single(x => x.weapon == CurrentWeapon);

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0)
        {
            var unlockedWeapons = _weapons.Where(x => x.unlocked);
            var idx = _weapons.IndexOf(_weapons.Single(x => x.selected));
            idx = idx == unlockedWeapons.Count() - 1 ? 0 : idx + 1;
            CurrentWeapon = unlockedWeapons.ElementAt(idx).weapon;
        }
        else if (wheel < 0)
        {
            var unlockedWeapons = _weapons.Where(x => x.unlocked);
            var idx = _weapons.IndexOf(_weapons.Single(x => x.selected));
            idx = idx == 0 ? unlockedWeapons.Count() - 1 : idx - 1;
            CurrentWeapon = unlockedWeapons.ElementAt(idx).weapon;
        }

        for (int i = 1; i <= 4; i++)
        {
            _SwitchWeaponAux(i);
        }
    }

    private void _SwitchWeaponAux(int id)
    {
        string input = "Weapon" + id.ToString();

        if (Input.GetAxis(input) <= 0)
            return;

        if (_weapons[id - 1].unlocked)
        {
            CurrentWeapon = _weapons[id - 1].weapon;
        }
    }

    private class WeaponData
    {
        public AWeapon weapon;
        public bool unlocked;
        public bool selected;

        public WeaponData(AWeapon weapon_, bool unlocked_)
        {
            weapon = weapon_;
            unlocked = unlocked_;
            selected = false;

        }
    }
}