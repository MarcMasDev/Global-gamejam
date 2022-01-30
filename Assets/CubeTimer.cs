using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTimer : Respawnable
{
    private Rigidbody rb;
    private float timer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.SetPos();
    }
    // Update is called once per frame
    void Update()
    {

        print(rb.velocity.magnitude);
        if (rb.velocity.magnitude < 0.5)
        {
            timer += Time.deltaTime;
        }
        print(timer);

        if (timer > 5)
        {
            timer = 0;
            base.Respawn();
        }
           
    }
}
