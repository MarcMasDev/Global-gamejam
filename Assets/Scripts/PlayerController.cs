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
    public float AimingSpeed;
    public float BlendMovement;
    public Image Image;
    public RejectArea HeadMag;
    public AtractArea MagArea;
    public Transform AimPoint;
    public Transform Head;
    public float DotActivateMagPlatform;

    private Vector3 _movementAxis;
    private Vector3 _movement;
    private float _verticalSpeed;
    private CharacterController _charController;
    private CameraController _camController;
    private Animator _animator;
    private GameObject _currentPlatform;
    private MagPlatformController _magPlatformController;

    private bool _onGround;
    private bool _attracting;
    private bool _ejecting;

    private float _speedAnimator;
    private float _fallTiming=0;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _camController = Cam.GetComponent<CameraController>();
        _animator = GetComponent<Animator>();
        Image.enabled = false;
        Head.transform.forward = Vector3.up;
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

        if (_attracting || _ejecting)
        {
            Head.LookAt(AimPoint);
        }
        //else
        //    Head.transform.forward = Vector3.up;

        Speed();

        Gravity();

        Attracting();

        Ejecting();

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

        if (_attracting || _ejecting)
        {
            speed = AimingSpeed;
        }

        _animator.SetFloat("Speed", _speedAnimator); 

        _movement = _movementAxis * speed * Time.deltaTime;
    }



    void Gravity()
    {
        _movement.y = _verticalSpeed * Time.deltaTime;
        CollisionFlags collisionFlags = _charController.Move(_movement);

        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            _onGround = true;
            _animator.SetBool("OnGround", true);
        }
        else if ((collisionFlags & CollisionFlags.Below) == 0)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MagArea.StartMagnetism();
                _attracting = true;
                _camController.Aiming(_attracting);
                _animator.SetBool("Attracting", _attracting);
                Image.enabled = true;

            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {

            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                MagArea.EndMagnetism();
                _attracting = false;
                _camController.Aiming(_attracting);
                _animator.SetBool("Attracting", _attracting);
                Image.enabled = false;
                //Head.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            }
        }
    }

    void Ejecting()
    {
        if (CanEject())
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                HeadMag.StartRejectism();
                _ejecting = true;
                _camController.Aiming(_ejecting);
                _animator.SetBool("Ejecting", _ejecting);
                Image.enabled = true;

            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {

            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                HeadMag.EndRejectism();
                _ejecting = false;
                _camController.Aiming(_ejecting);
                _animator.SetBool("Ejecting", _ejecting);
                Image.enabled = false;
              //  Head.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
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
        return _onGround && !_attracting && !_ejecting;
    }

    bool CanAttract()
    {
        return _onGround && !_ejecting;
    }

    bool CanEject()
    {
        return _onGround && !_attracting;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Platform" || other.tag == "MagPlatform") && _currentPlatform == null)
        {
            AttachElevator(other);
            if (other.tag == "MagPlatform")
            {
                _magPlatformController = _currentPlatform.GetComponent<MagPlatformController>();
                Debug.Log(_magPlatformController);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MagPlatform" && _currentPlatform == other.gameObject)
        {
            if (_ejecting || _attracting)
            {
                Vector3 colliderForward = other.transform.forward;
                colliderForward.y = 0;
                colliderForward.Normalize();
                float dot = Vector3.Dot(colliderForward, Head.transform.forward);
                if (dot > DotActivateMagPlatform)
                {
                    if (_ejecting)
                    {
                        _magPlatformController.MoveBack();
                    }
                    else if (_attracting)
                    {
                        _magPlatformController.MoveFront();
                    }
                }
                else if (dot < -DotActivateMagPlatform)
                {
                    if (_attracting)
                    {
                        _magPlatformController.MoveBack();
                    }
                    if (_ejecting)
                    {
                        _magPlatformController.MoveFront();
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Platform" || other.tag == "MagPlatform") && _currentPlatform == other.gameObject)
        {
            DetachElevator();
        _magPlatformController = null;
    }
    }

    void AttachElevator(Collider elevator)
    {
        _currentPlatform = elevator.gameObject;
        transform.SetParent(elevator.transform);

    }
    void DetachElevator()
    {
        _currentPlatform = null;
        transform.SetParent(null);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, eulerAngles.y, 0.0f);
    }

}
