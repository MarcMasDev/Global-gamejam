using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPlatformController : MonoBehaviour
{
    public Transform FrontPoint;
    public Transform BackPoint;
    private Rigidbody rigidbody;
    public float DotActivateMagPlatform;
    private bool _moveF;
    private bool _moveB;
    private bool _player;
    public float speed;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void MoveFront()
    {
        _moveF = true;
    }
    public void MoveBack()
    {
        _moveB = true;
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Vector3 newPos = Vector3.zero;
        if (_moveF)
        {
            newPos = transform.position + transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(newPos, FrontPoint.position) > 0.1)
            {
                Debug.Log(Vector3.Distance(newPos, FrontPoint.position));
                transform.position = newPos;
            }
        }
        else if (_moveB)
        {
            newPos = transform.position - transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(newPos, BackPoint.position) > 0.1)
            {
                transform.position = newPos;
            }
        }
        _moveF = false;
        _moveB = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _player = true;
        if (other.tag == "Interactable")
            other.transform.SetParent(transform);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!_player)
        {
            if (other.tag == "Eject" || other.tag == "Attract")
            {
                Vector3 colliderForward = other.transform.forward;
                colliderForward.y = 0;
                colliderForward.Normalize();
                float dot = Vector3.Dot(colliderForward, transform.forward);
                if (dot > DotActivateMagPlatform)
                {
                    if (other.tag == "Eject")
                    {
                        _moveF = true;
                    }
                    else if (other.tag == "Attract")
                    {
                        _moveB = true;
                    }
                }
                else if (dot < -DotActivateMagPlatform)
                {
                    if (other.tag == "Attract")
                    {
                        _moveF = true;
                    }
                    if (other.tag == "Eject")
                    {
                        _moveB = true;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _player = false;
        if (other.tag == "Interactable")
            other.transform.SetParent(null);
    }
}
