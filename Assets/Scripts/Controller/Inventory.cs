using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<AmmoTypeToAmmo_EditorRequired> _ammos;

    public Dictionary<Ammo.e_AmmoType, Ammo> ammos = new Dictionary<Ammo.e_AmmoType, Ammo>();

    void Awake()
    {
        ammos = _ammos.ToDictionary(x => x.type, x => x.ammo);

        ammos[Ammo.e_AmmoType.Blue].StockAmmo += 10;
    }

    [System.Serializable]
    private struct AmmoTypeToAmmo_EditorRequired
    {
        public Ammo.e_AmmoType type;
        public Ammo ammo;
    }
}
