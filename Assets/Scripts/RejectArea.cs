using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RejectArea : MonoBehaviour
{

    public ParticleSystem eject;
    public float Force= 1.6f;
    public float DistanceImpact;
    public float MaxDistanceImpact=4f;
    public ActionController ActionController;
    public Collider Head;

    private AudioSource RejectSound;
    public Transform HeadPos;
    public Transform ObjectAttachedMesh;

    //public List<GameObject> ObjectsAttached = new List<GameObject>();
    public GameObject ObjectAttached;

    private void Start()
    {
        RejectSound = GetComponent<AudioSource>();
        GameManager.GetManager().SetRejectarea(this);
    }


    public void Reject()
    {

        //if (ObjectsAttached.Count != 0)
        //{

        //for (int i = 0; i < ObjectsAttached.Count; i++)
        //{
        //    ObjectsAttached[i].transform.SetParent(null);

        //Rigidbody rb = ObjectsAttached[0].GetComponent<Rigidbody>();
        if (ObjectAttached != null)
        {

            ObjectAttached.SetActive(true);
            ObjectAttachedMesh.gameObject.GetComponent<MeshFilter>().mesh = null;
            Rigidbody rb = ObjectAttached.GetComponent<Rigidbody>();

            rb.isKinematic = false;
           // rb.AddForceAtPosition(Camera.main.transform.forward * Force * 2f, rb.transform.position, ForceMode.Impulse);
            ObjectAttached.transform.SetParent(null);
            ObjectAttached = null;

        }

        //rb.isKinematic = false;
        //rb.AddForceAtPosition(Camera.main.transform.forward * Force * 2f, rb.transform.position, ForceMode.Impulse);
        //ObjectsAttached.Remove(ObjectsAttached[i].gameObject);
        //}
        //}
    }

    public void AddRejectStatus(GameObject other)
    {
        //head mangent
        if (ObjectAttached != null)
        {
            other.transform.SetParent(transform);
            //GameManager.GetManager().GetRejectArea().ObjectsAttached.Add(other.gameObject);
            GameManager.GetManager().GetRejectArea().ObjectAttached = other.gameObject;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
       


    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            //if (!ObjectsAttached.Contains(other.gameObject))
            //{
                Rigidbody og = other.GetComponent<Rigidbody>();
                DistanceImpact = Vector3.Distance(other.transform.position, transform.position);
                DistanceImpact = Mathf.Clamp(DistanceImpact, 0, MaxDistanceImpact);
                og.AddForceAtPosition(Camera.main.transform.forward * Force * DistanceImpact, og.transform.position, ForceMode.Impulse);
            //}
        }


        if (other.CompareTag("MaxAtracted"))
        {
            //functionw
            other.GetComponent<MaxAtracted>().RemoveAtraction();
        }
    }

    public void StartRejectism()
    {
        
        
            Head.enabled = false;
           // RejectSound.PlayOneShot(RejectSound.clip);
            eject.gameObject.SetActive(true);
            eject.Play();
            ActionController.SetRejectTrigger();
            Reject();
        
       
    }
    public void EndRejectism()
    {
        Head.enabled = true;
        ActionController.OffRejectTrigger();
        eject.Stop();
        eject.gameObject.SetActive(false);
    }

}
