using UnityEngine;
using System.Collections;

public class Ammunition : APickUp
{
    public Ammo.e_AmmoType type;
    public int refillAmount;
	
    public override void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Avatar")
        {
            col.GetComponentInChildren<Inventory>().ammos[type].StockAmmo += refillAmount;
            Destroy(this.gameObject);
        }
    }
}
