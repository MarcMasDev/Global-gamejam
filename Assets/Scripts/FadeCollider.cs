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
}
