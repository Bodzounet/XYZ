using UnityEngine;
using System.Collections;

public class MasterAI : MonoBehaviour
{
    public GameObject fractured;

    Animator _anim;

    public Animator Anim
    {
        get { return _anim; }
        set
        {
            _anim = value;
        }
    }

    void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        this.GetComponent<EnemyHealthManager>().OnDie += Die;
    }

    public void Die()
    {
        GameObject go = GameObject.Instantiate(fractured, transform.GetChild(0).position, transform.rotation) as GameObject;
        //Destroy(go, 5);
        Destroy(this.gameObject);
    }
}
