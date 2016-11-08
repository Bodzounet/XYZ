using UnityEngine;
using System.Collections;

public class testSaveLoad : MonoBehaviour
{
    void Start()
    {
        var v = Save_Load.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Save_Load.Instance.SaveGame(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Save_Load.Instance.Load(1);
        }
    }
}
