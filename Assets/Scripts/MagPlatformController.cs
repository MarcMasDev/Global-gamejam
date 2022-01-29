using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPlatformController : MonoBehaviour
{
    public Transform FrontPoint;
    public Transform BackPoint;
    public float DotActivateMagPlatform;
    private bool _moveF;
    private bool _moveB;
    public float speed;

    private void Start()
    {
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
        Vector3 newPos = Vector3.zero;
        Debug.Log(_moveB + " " + _moveF);
        if (_moveF)
        {
            newPos = transform.position + transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(newPos, FrontPoint.position) > 0.1)
            {
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
    private void OnTriggerStay(Collider other)
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
                    Debug.Log("A");
                    _moveF = true;
                }
                else if (other.tag == "Attract")
                {
                    Debug.Log("A");
                    _moveB = true;
                }
            }
            else if (dot < -DotActivateMagPlatform)
            {
                if (other.tag == "Attract")
                {
                    Debug.Log("A");
                    _moveF = true;
                }
                if (other.tag == "Eject")
                {
                    Debug.Log("A");
                    _moveB = true;
                }
            }
        }
    }
}
