using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Collider Magnetic;
    public Collider Reject;


    private void Start()
    {
        Reject.enabled = false;
        Magnetic.enabled = false;
    }
    public void SetMagenticTrigger()
    {
        Reject.enabled = false;
        Magnetic.enabled = true;
    }

    public void OffMagneticTrigger()
    {

        
        Magnetic.enabled = false;
    }

    public void SetRejectTrigger()
    {

        Reject.enabled = true;
        Magnetic.enabled = false;

    }

    public void OffRejectTrigger()
    {

        Reject.enabled = false;
    }
}
