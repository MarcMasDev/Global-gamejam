using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform FrontPoint;
    public Transform BackPoint;
    private bool _moveF = true;
    private bool _moveB;
    public float speed;
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
            else
            {
                _moveF = false;
                _moveB = true;
            }
        }
        else if (_moveB)
        {
            newPos = transform.position - transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(newPos, BackPoint.position) > 0.1)
            {
                transform.position = newPos;
            }
            else
            {
                _moveF = true;
                _moveB = false;
            }
        }
    }
}
