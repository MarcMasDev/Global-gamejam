using System.Collections.Generic;
using UnityEngine;

public class HeadMagnet : MonoBehaviour
{

    public ParticleSystem eject;
    public float Force;
    public float DistanceImpact;
    public float MaxDistanceImpact=4f;
    public ActionController ActionController;

    private void Start()
    {
      
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
        {
            eject.Play();
            ActionController.SetRejectTrigger();
        }
          
        if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
        {
            ActionController.OffMagneticTrigger();
            eject.Stop();
        }
           
        


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
            //og.AddForce(m * Force * DistanceImpact, ForceMode.Impulse);
            og.AddRelativeForce(new Vector3(og.transform.position.x, og.transform.position.y, og.transform.position.z) * Force * DistanceImpact, ForceMode.Impulse);
        }
    }

}
