using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class UI_Ammo : MonoBehaviour
    {

        private Text _text;

        void Awake()
        {
            _text = GetComponentInChildren<Text>();
            FindObjectOfType<WeaponController>().OnWeaponChanged += OnWeaponChanged;
        }

        void OnWeaponChanged(AWeapon oldWeapon, AWeapon newWeapon)
        {
            if (oldWeapon != null)
                newWeapon.Clip.Inventory.ammos[newWeapon.Clip.ammoType].OnStockAmmoChanged -= OnAmmoChanged;
            newWeapon.Clip.Inventory.ammos[newWeapon.Clip.ammoType].OnStockAmmoChanged += OnAmmoChanged;

            OnAmmoChanged(newWeapon.Clip.Inventory.ammos[newWeapon.Clip.ammoType].StockAmmo, newWeapon.Clip.Inventory.ammos[newWeapon.Clip.ammoType].MaxAmmo);
        }

        void OnAmmoChanged(int newStock, int maxStock)
        {
            _text.text = newStock.ToString() + " / " + maxStock.ToString();
        }
    }
}