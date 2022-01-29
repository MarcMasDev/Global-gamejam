using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedMagnetismHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Interactable"))
        {
        
                GameManager.GetManager().GetRejectArea().AddRejectStatus(other.gameObject);
              
        }
    }
}
