using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtractArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float MagnetDuration=1f;
    public float currentDuration = 0f;

    public Transform Pos;
    public Transform ObjectAttachedMesh;
    public Transform RejectAreaParent;
    public ParticleSystem Mangentism;
    public ActionController ActionController;
    
    //private float timer;
    void Start()
    {
        currentDuration = 0;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Interactable") && GameManager.GetManager().GetRejectArea().ObjectAttached == null)
        {
            currentDuration += Time.deltaTime;
            float l_Pct = Mathf.Min(1, currentDuration / MagnetDuration);
            other.transform.position = Vector3.Lerp(other.transform.position, Pos.position, l_Pct / MagnetDuration);

            if ((l_Pct >= 0.7 && l_Pct <= 1) && GameManager.GetManager().GetRejectArea().ObjectAttached == null)//!GameManager.GetManager().GetRejectArea().ObjectsAttached.Contains(other.gameObject))
            {
                other.transform.SetParent(RejectAreaParent);
                //GameManager.GetManager().GetRejectArea().ObjectsAttached.Add(other.gameObject);
                GameManager.GetManager().GetRejectArea().ObjectAttached = other.gameObject;
                other.GetComponent<Rigidbody>().isKinematic = true;


                if (other.GetComponent<Cube>())
                {
                    other.gameObject.SetActive(false);
                    ObjectAttachedMesh.GetComponent<MeshFilter>().mesh = other.GetComponent<MeshFilter>().mesh;
                }


            }
        }
        if (other.CompareTag("MaxAtracted"))
        {
            //function
            other.GetComponent<MaxAtracted>().AddAtraction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentDuration = 0;
        }
    }

    public void StartMagnetism()
    {
        ActionController.SetMagenticTrigger();
       
        Mangentism.gameObject.SetActive(true);
        Mangentism.Play();
        print("A");
        //timer = 0;
        currentDuration = 0;
    }

    public void EndMagnetism()
    {
        ActionController.OffMagneticTrigger();
        //timer = 0;
        Mangentism.Stop();
        Mangentism.gameObject.SetActive(false);
        currentDuration = 0;
    }
}
