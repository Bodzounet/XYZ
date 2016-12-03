using UnityEngine;
using System.Collections;

public class testDamages : MonoBehaviour {

    AvatarHealthManager ahm;

	// Use this for initialization
	void Start ()
    {
        ahm = FindObjectOfType<AvatarHealthManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.U))
        {
            ahm.Armor += 10;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ahm.Life += 10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ahm.TakeDamages(10);
        }
    }
}
