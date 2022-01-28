using UnityEngine;
public class CameraController : MonoBehaviour
{
    [Header("Normal")]
    public Transform LookAtNormal;
    public float MinDistanceNormal;
    public float MaxDistanceNormal;
    public float YawRotationSpeedNormal;
    public float PitchRotationSpeedNormal;
    [Header("Attracting")]
    public Transform LookAtAttracting;
    public float MinDistanceAttracting;
    public float MaxDistanceAttracting;
    public float YawRotationSpeedAttracting;
    public float PitchRotationSpeedAttracting;
    [Header("Common")]
    public float MinPitch;
    public float MaxPitch;
    public LayerMask LayerMask;
    public float LerpCamera;

    public float Yaw;

    private Transform _lookAt;
    private float _minDistance;
    private float _maxDistance;
    private float _yawRotationSpeed;
    private float _pitchRotationSpeed;

    private Vector3 _direction;
    private float _pitch;
    private bool _adjust;

    private void Awake()
    {
        GameManager.GetManager().SetCamera(this);
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Attracting(false);
    }
    private void LateUpdate()
    {
        _direction = _lookAt.position - transform.position;
        float distance = _direction.magnitude;
        distance = Mathf.Clamp(distance, _minDistance, _maxDistance);
        if (_adjust)
        {
            distance = _maxDistance;
            _adjust = false;
        }
        _direction.y = 0.0f;
        _direction.Normalize();

        Yaw = Mathf.Atan2(_direction.x, _direction.z);

        float translation = Input.GetAxis("Mouse Y");
        float rotation = Input.GetAxis("Mouse X");

        Yaw += rotation * _yawRotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        _pitch += translation * _pitchRotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, MinPitch * Mathf.Deg2Rad, MaxPitch * Mathf.Deg2Rad);

        _direction = new Vector3(Mathf.Sin(Yaw) * Mathf.Cos(_pitch), Mathf.Sin(_pitch),
            Mathf.Cos(Yaw) * Mathf.Cos(_pitch));
        Vector3 desiredPostion = _lookAt.position - _direction * distance;
        Ray ray = new Ray(_lookAt.position, -_direction);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, LayerMask.value))
        {
            Debug.Log(raycastHit.collider.name);
            desiredPostion = raycastHit.point;
        }

        transform.position = desiredPostion;
        transform.LookAt(_lookAt);

        //transform.position = Vector3.Lerp(transform.position, desiredPostion, 1);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), LerpCamera);
    }

    public void Attracting(bool b)
    {
        if (!b)
        {
            _lookAt = LookAtNormal;
            _minDistance = MinDistanceNormal;
            _maxDistance = MaxDistanceNormal;
            _yawRotationSpeed = YawRotationSpeedNormal;
            _pitchRotationSpeed = PitchRotationSpeedNormal;
            _adjust = true;
        }
        else
        {
            _lookAt = LookAtAttracting;
            _minDistance = MinDistanceAttracting;
            _maxDistance = MaxDistanceAttracting;
            _yawRotationSpeed = YawRotationSpeedAttracting;
            _pitchRotationSpeed = PitchRotationSpeedAttracting;
        }
    }
}
