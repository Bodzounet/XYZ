using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour
{
    AvatarController _controller;

    void Awake()
    {
        _controller = GetComponentInParent<AvatarController>();
    }

    void OnTriggerEnter(Collider col)
    {
        _controller.IsJumping = false;
    }

    void OnTriggerExit(Collider col)
    {
        _controller.IsJumping = true;
    }
}
