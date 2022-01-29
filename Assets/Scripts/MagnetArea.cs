using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float MagnetDuration=2.5f;
    public float currentDuration = 0f;

    public Transform headPos;
    public ParticleSystem Mangentism;
    private float timer;
    public ActionController ActionController;
    void Start()
    {
        currentDuration = 0;
    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.CompareTag("Interactable") || other.CompareTag("Cube")))
        {

            currentDuration += Time.deltaTime;
            float l_Pct = Mathf.Min(1, currentDuration / MagnetDuration);
            other.transform.position = Vector3.Lerp(other.transform.position, headPos.position, currentDuration / MagnetDuration);
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
        Mangentism.Play();
        timer = 0;
        currentDuration = 0;
    }

    public void EndMagnetism()
    {
        ActionController.OffMagneticTrigger();
        timer = 0;
        Mangentism.Stop();
        currentDuration = 0;
    }
}
