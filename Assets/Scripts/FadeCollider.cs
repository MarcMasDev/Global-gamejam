using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class FadeCollider : MonoBehaviour
{
    public Animator AnimatorFade;
    private bool _open;

    private AudioSource _audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_open)
            {
                AnimatorFade.SetTrigger("Exit");
                other.GetComponent<CharacterController>().enabled = false;
            }
        }
    }
    private void Update()
    {
        if (AnimatorFade.GetBool("Done"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
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
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.PlayOneShot(_audioSource.clip);
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

        _audioSource.PlayOneShot(_audioSource.clip);
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
        _open = true;
    }

    public void ClosePortal()
    {
        
        StopAllCoroutines();
        StartCoroutine(RemoveIntensity());
        _open = false;
    }
}
