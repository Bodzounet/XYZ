using UnityEngine;
using System.Collections;

public class InitSingleton : MonoBehaviour
{
	void Awake ()
    {
        var v = LevelManager.Instance;
        var w = Save_Load.Instance;
        Destroy(this.gameObject);
    }
}
