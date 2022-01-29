using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCollider : MonoBehaviour
{
    public Animator AnimatorFade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AnimatorFade.SetTrigger("Exit");
        }
    }
    private void Update()
    {
        if (AnimatorFade.GetBool("Done"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    //material variables.

    
    public Material mr;
    public float ZeroIntensity=0;
    public float OneIntensity=1;
    public GameObject VSF_Portal;
    // Vector1_4b6b6a13d6244e75ae374d22e31829b8 <- intesity variable of perticles
    // Vector1_564c2d8ba9384ad59570b221315de88d <- ""        ""  of portal material;

    private void Start()
    {
        mr.SetFloat("Vector1_564c2d8ba9384ad59570b221315de88d", ZeroIntensity);
        //Particles
        VSF_Portal.SetActive(false);
    }

    //private void Debuggin()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        print("a  "+ VSF_Portal);
    //        VSF_Portal.SetActive(false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        StartCoroutine(DelayIntesity());
    //    }
    //}

    private IEnumerator DelayIntesity()
    {
        VSF_Portal.SetActive(true);
        for (float i = 0; i < OneIntensity;)
        {
            yield return new WaitForSeconds(0.2f);
            
            i += 0.1f;
            mr.SetFloat("Vector1_564c2d8ba9384ad59570b221315de88d", i);
        }
    }

    private IEnumerator RemoveIntensity()
    {
        VSF_Portal.SetActive(false);
        for (float i = OneIntensity; i > ZeroIntensity;)
        {
            yield return new WaitForSeconds(0.1f);
            i -= 0.2f;
            mr.SetFloat("Vector1_564c2d8ba9384ad59570b221315de88d", i);
        }
    }
    public void OpenPortal()
    {
        StartCoroutine(DelayIntesity());
    }

    public void ClosePortal()
    {
        StopAllCoroutines();
        StartCoroutine(RemoveIntensity());
    }
}
