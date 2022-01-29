using System.Collections.Generic;
using UnityEngine;

public class RejectArea : MonoBehaviour
{

    public ParticleSystem eject;
    public float Force= 1.6f;
    public float DistanceImpact;
    public float MaxDistanceImpact=4f;
    public ActionController ActionController;
    public Collider Head;

    public List<GameObject> ObjectsAttached = new List<GameObject>();

    private void Start()
    {
        GameManager.GetManager().SetRejectarea(this);
    }


    public void Reject()
    {
       
        if (ObjectsAttached.Count != 0)
        {
           
            for (int i = 0; i < ObjectsAttached.Count; i++)
            {
                ObjectsAttached[i].transform.SetParent(null);
                
                Rigidbody rb = ObjectsAttached[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForceAtPosition(Camera.main.transform.forward * Force * 2f, rb.transform.position, ForceMode.Impulse);
                ObjectsAttached.Remove(ObjectsAttached[i].gameObject);
            }
        }
    }

    public void AddRejectStatus(GameObject other)
    {
        //head mangent
        other.transform.SetParent(transform);
        GameManager.GetManager().GetRejectArea().ObjectsAttached.Add(other.gameObject);
        other.GetComponent<Rigidbody>().isKinematic = true;


    }
    private void OnTriggerStay(Collider other)
    {
        if((other.CompareTag("Interactable") || other.CompareTag("Cube")))
        {
            if (!ObjectsAttached.Contains(other.gameObject))
            {
                Rigidbody og = other.GetComponent<Rigidbody>();
                DistanceImpact = Vector3.Distance(other.transform.position, transform.position);
                DistanceImpact = Mathf.Clamp(DistanceImpact, 0, MaxDistanceImpact);
                og.AddForceAtPosition(Camera.main.transform.forward * Force * DistanceImpact, og.transform.position, ForceMode.Impulse);
            }
        }
        print(other.tag);
        if (other.CompareTag("MaxAtracted"))
        {
            //function
            print("atracted");
            other.GetComponent<MaxAtracted>().RemoveAtraction();
        }
    }

    public void StartRejectism()
    {
        Head.enabled = false;
        eject.Play();
        ActionController.SetRejectTrigger();
        Reject();
    }
    public void EndRejectism()
    {
        Head.enabled = true;
        ActionController.OffMagneticTrigger();
        eject.Stop();
    }

}
