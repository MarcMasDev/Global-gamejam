using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Palanca : MonoBehaviour
{
    public float DotActivatePalanca;
    public bool On;
    private Animator _animator;
    public UnityEvent OnEvent;
    public UnityEvent OffEvent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Eject" || other.tag == "Attract")
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
                        OffEvent.Invoke();
                    }
                }
                if (!On)
                {
                    if (other.tag == "Attract")
                    {
                        _animator.SetTrigger("On");
                        On = true;
                        OnEvent.Invoke();
                    }
                }
            }
            else if (dot < -DotActivatePalanca)
            {
                if (On)
                {
                    if (other.tag == "Attract")
                    {
                        _animator.SetTrigger("Off");
                        On = false;
                        OffEvent.Invoke();
                    }
                }
                if (!On)
                {
                    if (other.tag == "Eject")
                    {
                        _animator.SetTrigger("On");
                        On = true;
                        OnEvent.Invoke();
                    }
                }
            }
        }
    }
}
