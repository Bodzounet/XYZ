﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponController))]
public class AvatarController : MonoBehaviour
{
    private SwitchTrigger.ActivateSwitchTrigger switchTrigger;
    public SwitchTrigger.ActivateSwitchTrigger SwitchTrigger
    {
        get { return switchTrigger; }
        set
        {
            switchTrigger = value;
        }
    }

    private bool canUseAction = true;

    public enum e_MovementDirection
    {
        Left,
        Right,
        Forward,
        Backward,

        None
    }

    [SerializeField]
    private float _baseWalkingSpeed;
    public float BaseWalkingSpeed
    {
        get { return _baseWalkingSpeed; }
    }

    private float _walkingSpeed;
    public float WalkingSpeed
    {
        get { return _walkingSpeed; }
        set
        {
            _walkingSpeed = value;
        }
    }

    [SerializeField]
    private float _baseRunningSpeed;
    public float BaseRunningSpeed
    {
        get { return _baseRunningSpeed; }
    }

    private float _runningSpeed;
    public float RunningSpeed
    {
        get { return _runningSpeed; }
        set
        {
            _runningSpeed = value;
        }
    }

    [SerializeField]
    private float _baseJumpingSpeed;
    public float BaseJumpingSpeed
    {
        get { return _baseJumpingSpeed; }
    }

    private float _jumpingSpeed;
    public float JumpingSpeed
    {
        get { return _jumpingSpeed; }
        set
        {
            _jumpingSpeed = value;
        }
    }

    [SerializeField]
    private float _rotationSpeed;

    private bool _isRunning = false;
    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
        }
    }

    private bool _isJumping = false;
    public bool IsJumping
    {
        get { return _isJumping; }
        set
        {
            _isJumping = value;
        }
    }

    private Rigidbody _rgbd;


    void Awake()
    {
        _rgbd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        WalkingSpeed = BaseWalkingSpeed;
        RunningSpeed = BaseRunningSpeed;
        JumpingSpeed = BaseJumpingSpeed;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        _Move();
        _Jump();
        _Look();
        _Switch();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void _Look()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }

    private void _Jump()
    {
        if (!IsJumping && Input.GetAxis("Jump") > 0)
        {
            _rgbd.velocity += new Vector3(0, JumpingSpeed, 0);
            IsJumping = true;
        }
    }

    private void _Move()
    {
        Vector3 movement = Vector3.zero;

        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            movement = _MoveAux(x > 0 ? e_MovementDirection.Right : e_MovementDirection.Left);

        float z = Input.GetAxis("Vertical");
        if (z != 0)
            movement += _MoveAux(z > 0 ? e_MovementDirection.Forward : e_MovementDirection.Backward);

        movement = movement.normalized * (_isRunning ? RunningSpeed : WalkingSpeed);
        _rgbd.velocity = new Vector3(movement.x, _rgbd.velocity.y, movement.z);
    }

    private Vector3 _MoveAux(e_MovementDirection dir)
    {
        switch (dir)
        {
            case e_MovementDirection.Forward:
                return transform.forward;
            case e_MovementDirection.Backward:
                return -transform.forward;
            case e_MovementDirection.Right:
                return transform.right;
            case e_MovementDirection.Left:
                return -transform.right;
        }
        return Vector3.zero;
    }

    private void _Switch()
    {
        if (Input.GetAxis("Action") > 0)
        {
            if (canUseAction)
            {
                if (SwitchTrigger != null)
                    SwitchTrigger();
            }
            canUseAction = false;
        }
        else
        {
            canUseAction = true;
        }
    }
}
