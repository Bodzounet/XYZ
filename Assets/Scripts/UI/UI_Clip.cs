using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class UI_Clip : MonoBehaviour
    {
        private Text _text;

        void Awake()
        {
            _text = GetComponentInChildren<Text>();
            FindObjectOfType<AvatarController>().OnWeaponChanged += OnWeaponChanged;
        }

        void OnWeaponChanged(AWeapon oldWeapon, AWeapon newWeapon)
        {
            if (oldWeapon != null)
                oldWeapon.Clip.OnStockClipChanged -= OnAmmoChanged;

            newWeapon.Clip.OnStockClipChanged += OnAmmoChanged;

            OnAmmoChanged(newWeapon.Clip.AmmoRemaining, newWeapon.Clip.MaxCapacity);
        }

        void OnAmmoChanged(int newStock, int maxStock)
        {
            _text.text = newStock.ToString() + " / " + maxStock.ToString();
        }
    }
}