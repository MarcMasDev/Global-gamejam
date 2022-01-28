using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    public UnityEvent ActiveEvent;
    public UnityEvent DesactiveEvent;



    public void OnCollisionEnter(Collision collision)
    {
       
          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cube"))
        {
            ActiveEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //per si de cass
    }
}
