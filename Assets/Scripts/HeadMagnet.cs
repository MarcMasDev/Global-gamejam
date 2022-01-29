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

    private void OnTriggerStay(Collider other)
    {
        if((other.CompareTag("Interactable") || other.CompareTag("Cube")))
        {
        
            Rigidbody og = other.GetComponent<Rigidbody>();
            print(og.name);
            DistanceImpact = Vector3.Distance(other.transform.position, transform.position);

            DistanceImpact = Mathf.Clamp(DistanceImpact, 0, MaxDistanceImpact);
            //og.AddForce(m * Force * DistanceImpact, ForceMode.Impulse);
            og.AddRelativeForce(new Vector3(og.transform.position.x, og.transform.position.y, og.transform.position.z) * Force * DistanceImpact, ForceMode.Impulse);
        }
    }

    public void StartMagnetism()
    {
        eject.Play();
        ActionController.SetRejectTrigger();
    }
    public void EndMagnetism()
    {
        ActionController.OffMagneticTrigger();
        eject.Stop();
    }

}
