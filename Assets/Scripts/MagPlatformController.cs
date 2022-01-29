using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPlatformController : MonoBehaviour
{
    private bool _moveF;
    private bool _moveB;
    public float speed;
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
        }
        else if (_moveB)
        {
            newPos = transform.position - transform.forward * speed * Time.deltaTime;
        }

        transform.position = newPos;

        _moveF = false;
        _moveB = false;
    }
}
