using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera Cam;
    public float WalkSpeed;
    //public float RunSpeed;
    public float LerpRotationPercentatge;
    public float JumpSpeed;
    public float BlendMovement;
    public Image Image;

    private Vector3 _movementAxis;
    private Vector3 _movement;
    private float _verticalSpeed;
    private CharacterController _charController;
    private CameraController _camController;
    private Animator _animator;

    private bool _onGround;
    private bool _attracting;

    private float _speedAnimator;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _camController = Cam.GetComponent<CameraController>();
        _animator = GetComponent<Animator>();
        Image.enabled = false;
    }
    private void Start()
    {
        GameManager.GetManager().SetPlayer(this);
    }

    void Update()
    {
        Movement();

        if (_movementAxis != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_movementAxis), LerpRotationPercentatge * Time.deltaTime);

        Speed();

        Gravity();

        Attracting();

        Jump();
    }

    void Movement()
    {
        Vector3 forward = Cam.transform.forward;
        Vector3 right = Cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        _movementAxis = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _movementAxis = -right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _movementAxis = right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _movementAxis += forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _movementAxis -= forward;
        }

        if (_movementAxis != Vector3.zero)
        {
            _speedAnimator += BlendMovement;
        }
        else
        {
            _speedAnimator -= BlendMovement;
        }

        _movementAxis.Normalize();
    }
    void Speed()
    {
        float speed = WalkSpeed;
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    speed = RunSpeed;
        //}

        _animator.SetFloat("Speed", _speedAnimator); 

        _movement = _movementAxis * speed * Time.deltaTime;
    }

    void Gravity()
    {
        _movement.y = _verticalSpeed * Time.deltaTime;
        _verticalSpeed += Physics.gravity.y * Time.deltaTime;

        CollisionFlags collisionFlags = _charController.Move(_movement);
        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            _onGround = true;
            _animator.SetBool("OnGround", true);
        }
        else if ((collisionFlags & CollisionFlags.Below) == 0)
        {
            _onGround = false;
            _animator.SetBool("OnGround", false);
            //animator
        }

        if ((collisionFlags & CollisionFlags.Above) != 0 && _verticalSpeed > 0.0f)
            _verticalSpeed = 0.0f;
    }

    void Attracting()
    {
        if (CanAttract())
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _attracting = true;
                _camController.Attracting(_attracting);
                _animator.SetBool("Attracting", _attracting);
                Image.enabled = true;

            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {

            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                _attracting = false;
                _camController.Attracting(_attracting);
                _animator.SetBool("Attracting", _attracting);
                Image.enabled = false;
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            //animator

            _verticalSpeed = JumpSpeed;
        }

        _animator.SetBool("Falling", _verticalSpeed < 0);
    }

    bool CanJump()
    {
        return _onGround && !_attracting;
    }

    bool CanAttract()
    {
        return _onGround;
    }
}
