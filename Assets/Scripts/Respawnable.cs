using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    private void Start()
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
    }
}
