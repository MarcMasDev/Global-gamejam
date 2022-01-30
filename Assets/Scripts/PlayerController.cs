using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public Camera Cam;
    public float WalkSpeed;
    //public float RunSpeed;
    public float LerpRotationPercentatge;
    public float JumpSpeed;
    public float AimingSpeed;
    public float BlendMovement;
    public RejectArea HeadMag;
    public AtractArea MagArea;
    public float DotActivateMagPlatform;
    public GameObject Head;
    public Pointer Pointer;
    public GameObject Target;

    private Vector3 _movementAxis;
    private Vector3 _movement;
    private float _verticalSpeed;
    private CharacterController _charController;
    private CameraController _camController;
    public Animator Animator;
    private GameObject _currentPlatform;
    private MagPlatformController _magPlatformController;

    private bool _onGround;
    private bool _attracting;
    private bool _ejecting;

    private float _speedAnimator;

    private AudioSource _audioSource; 
    public AudioClip Step1, Step2;
    public AudioClip Jump1;

    public LowPassFilter LPF;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _charController = GetComponent<CharacterController>();
        _camController = Cam.GetComponent<CameraController>();
    }
    private void Start()
    {
        GameManager.GetManager().SetPlayer(this);
        Head.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
    }

    void Update()
    {
        Movement();

        if (_movementAxis != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_movementAxis), LerpRotationPercentatge * Time.deltaTime);

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
            //_speedAnimator += BlendMovement;
            _speedAnimator = 1;
        }
        else
        {
            //_speedAnimator -= BlendMovement;
            _speedAnimator = 0;
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

        Animator.SetFloat("Speed", _speedAnimator); 

        _movement = _movementAxis * speed * Time.deltaTime;
    }



    void Gravity()
    {
        _movement.y = _verticalSpeed * Time.deltaTime;
        CollisionFlags collisionFlags = _charController.Move(_movement);

        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            _onGround = true;
            Animator.SetBool("OnGround", true);
        }
        else if ((collisionFlags & CollisionFlags.Below) == 0)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
            _onGround = false;
            Animator.SetBool("OnGround", false);
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
                MagArea.StartMagnetism();
                _attracting = true;
                _camController.Aiming(_attracting);
                Pointer.ShowMinus();
                LPF.ModifiyPassFilter();
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                Head.transform.LookAt(Target.transform);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                MagArea.EndMagnetism();
                _attracting = false;
                _camController.Aiming(_attracting);
                Pointer.Hide();
                LPF.ResetPassFilter();
                Head.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            }
        }
    }

    void Ejecting()
    {
        if (CanEject())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HeadMag.StartRejectism();
                _ejecting = true;
                _camController.Aiming(_ejecting);
                Pointer.ShowPlus();
                LPF.ModifiyPassFilter();
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                Head.transform.LookAt(Target.transform);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                HeadMag.EndRejectism();
                _ejecting = false;
                _camController.Aiming(_ejecting);
                Pointer.Hide();
                LPF.ResetPassFilter();
                Head.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump() )
        {
            //animator
            _verticalSpeed = JumpSpeed;
        }

        Animator.SetBool("Falling", _verticalSpeed < 0);
    }

    bool CanJump()
    {
        return _onGround && !_attracting && !_ejecting && GameManager.GetManager().GetRejectArea().ObjectAttached == null;
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
                float dot = Vector3.Dot(colliderForward, HeadMag.transform.forward);
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



    //sounds.


    public void StepSound(int e)
    {
        if (e == 0)
            _audioSource.PlayOneShot(Step1);
        else
            _audioSource.PlayOneShot(Step2);
    }

    public void JumpSound()
    {
        _audioSource.PlayOneShot(Jump1);
    }
}
