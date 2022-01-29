using System.Collections;
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

    //public List<GameObject> ObjectsAttached = new List<GameObject>();
    public GameObject ObjectAttached;
    private bool canPush = true;

    private void Start()
    {
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
            Rigidbody rb = ObjectAttached.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            //rb.AddForceAtPosition(Camera.main.transform.forward * Force, rb.transform.position, ForceMode.Impulse);
            rb.velocity = Vector3.zero;
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
            StartCoroutine(SetCanReject());
        }
       


    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Interactable") && canPush)
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
    private IEnumerator SetCanReject()
    {
        canPush = false;
        yield return new WaitForSeconds(0.2f);
        canPush = true;
    }

}
