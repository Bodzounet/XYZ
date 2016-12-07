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
        fractured.SetActive(true);
        fractured.transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
