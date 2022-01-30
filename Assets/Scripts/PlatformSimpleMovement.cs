using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSimpleMovement : MonoBehaviour
{
    public Transform first, second;
    public bool ActivePlatf;
    private Transform current;

    private Vector3 _dir;
    private float speed = 1.4f;

    private bool right, left;
    private void Start()
    {
        left = true;
        On();
    }

    public void Update()
    {
        if (ActivePlatf)
        {

           
        }
        
    }

    public void On()
    {
        ActivePlatf = true;
    }

    public void Off()
    {
        ActivePlatf = false;
    }
}
