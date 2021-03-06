using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Palanca : MonoBehaviour
{
    public float DotActivatePalanca;
    public bool On=false;
    private Animator _animator;
    public UnityEvent OnEvent;
    public UnityEvent OffEvent;

    private AudioSource _audioSource;
    public AudioClip _onC, _offC;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null  && (other.CompareTag("Eject" )|| other.CompareTag("Attract")))
        {
           
            Vector3 colliderForward = other.transform.forward;
            colliderForward.y = 0;
            colliderForward.Normalize();
            float dot = Vector3.Dot(colliderForward, transform.forward);
            if (dot > DotActivatePalanca)
            {
               
                if (On)
                {
                    
                    if (other.tag == "Eject")
                    {
                        _animator.SetTrigger("Off");
                        On = false;
                        OffEvent?.Invoke();
                    }
                }
                if (!On)
                {
                    if (other.tag == "Attract")
                    {
                        _animator.SetTrigger("On");
                        On = true;
                        OnEvent?.Invoke();
                    }
                }

               
            }
            else if (dot < -DotActivatePalanca)
            {
                print(other.name + "www");
                if (On)
                {
                    if (other.tag == "Attract")
                    {
                        _animator.SetTrigger("Off");
                        On = false;
                        OffEvent?.Invoke();
                    }
                }
                if (!On)
                {
                    if (other.tag == "Eject")
                    {
                        _animator.SetTrigger("On");
                        On = true;
                        OnEvent?.Invoke();
                    }
                }
            }
        }
    }

    public void Sound(int a)
    {
        if (a == 0)
            _audioSource.PlayOneShot(_onC);
        else
            _audioSource.PlayOneShot(_offC);
    }
}
