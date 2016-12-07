using UnityEngine;
using System.Collections.Generic;

public class HeadBobbing : MonoBehaviour
{
    Animator _anim;
    Rigidbody _rgbd;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rgbd = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        _anim.SetBool("Move", _rgbd.velocity.x != 0);
    }
}
