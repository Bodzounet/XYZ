using UnityEngine;
using System.Collections;

public class AnimCallBack : MonoBehaviour
{
    public void CanCharge()
    {
        GetComponentInParent<AI.Attack_CrawlingCrystal>().CanCharge();
    }
}
