using System.Collections.Generic;
using UnityEngine;

public class HeadMagnet : MonoBehaviour
{

    public ParticleSystem eject;
    public float Force;
    public float DistanceImpact;
    public float MaxDistanceImpact=4f;

    private void Start()
    {
      
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
            eject.Play();
        if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
            eject.Stop();
        


        //if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        //{
        //    for (int m = 0; m < MagneticObjects.Count; m++)
        //    {
        //        MagneticObjects[m].transform.SetParent(null);
        //        Rigidbody og = MagneticObjects[m].GetComponent<Rigidbody>();
        //        og.isKinematic = false;
        //        MagneticObjects.Remove(og.gameObject);
        //        og.AddRelativeForce(transform.right * Force, ForceMode.Impulse);
        //    }
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if((other.CompareTag("Interactable") || other.CompareTag("Cube")) && Input.GetMouseButton(1))
        {
        
            Rigidbody og = other.GetComponent<Rigidbody>();
            print(og.name);
            DistanceImpact = Vector3.Distance(other.transform.position, transform.position);

            DistanceImpact = Mathf.Clamp(DistanceImpact, 0, MaxDistanceImpact);
            Vector3 camForward = GameManager.GetManager().GetCamera().transform.forward;
            //og.AddForce(m * Force * DistanceImpact, ForceMode.Impulse);
            og.AddRelativeForce(camForward * Force * DistanceImpact, ForceMode.Impulse);
            print(camForward * Force * DistanceImpact);
        }
    }

}
