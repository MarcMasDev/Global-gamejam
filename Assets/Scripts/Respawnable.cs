using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody.GetComponent<Rigidbody>();
        SetPos();
    }
    public void SetPos()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "FallZone")
        {
            Respawn();
        }
    }

    protected void Respawn()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

    }
}
