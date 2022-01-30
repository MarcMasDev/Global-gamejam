using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        print(rb.velocity.magnitude);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FallZone")
        {
            Respawn();
        }
        if (other.CompareTag("Wall")&& (rb.velocity.magnitude < 1))
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
