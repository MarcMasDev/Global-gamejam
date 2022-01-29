using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPlatformController : MonoBehaviour
{
    public Transform FrontPoint;
    public Transform BackPoint;
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
}
