using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    private AudioSource _atractSound;
    public Material shader;
    
    //private float timer;
    void Start()
    {
        _atractSound = GetComponent<AudioSource>();
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
                
                Texture e = other.GetComponent<MeshRenderer>().materials[0].mainTexture;
                other.transform.SetParent(RejectAreaParent);
                //GameManager.GetManager().GetRejectArea().ObjectsAttached.Add(other.gameObject);
                GameManager.GetManager().GetRejectArea().ObjectAttached = other.gameObject;
                other.GetComponent<Rigidbody>().isKinematic = true;


                if (other.GetComponent<Cube>())
                {
                    other.gameObject.SetActive(false);
                    ObjectAttachedMesh.GetComponent<MeshFilter>().mesh = other.GetComponent<MeshFilter>().mesh;
                    //ObjectAttachedMesh.GetComponent<MeshRen//SetTexture("Texture2D_e57cbf52cbeb4ccfbc1c157cfbe221ca", e);
                    print(shader.GetTexture("Texture2D_e57cbf52cbeb4ccfbc1c157cfbe221ca"));
                    shader.SetTexture("Texture2D_e57cbf52cbeb4ccfbc1c157cfbe221ca", e);
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
       // _atractSound.PlayOneShot(_atractSound.clip);
        Mangentism.gameObject.SetActive(true);
        Mangentism.Play();
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
