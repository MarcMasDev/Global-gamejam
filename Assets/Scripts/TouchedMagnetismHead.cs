using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedMagnetismHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube") || other.CompareTag("Interactable"))
        {
            if (!GameManager.GetManager().GetRejectArea().ObjectsAttached.Contains(other.gameObject))
                GameManager.GetManager().GetRejectArea().AddRejectStatus(other.gameObject);
              
        }
    }
}
