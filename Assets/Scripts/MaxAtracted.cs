using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAtracted : MonoBehaviour
{
    bool broken;
    public Transform LimitDistance;
    public Transform Init;

    public void AddAtraction()
    {

        if (transform.position.x< LimitDistance.transform.position.x && !broken )
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
          
    }

    public void RemoveAtraction()
    {
        print(transform.position.x);

        if (transform.position.x > Init.transform.position.x && !broken)
        {
            print("AAAAAAAAAA");
            transform.position -= new Vector3(0.1f, 0, 0);
        }
    }
}
