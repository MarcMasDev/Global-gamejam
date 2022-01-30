using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour
{
    public UnityEvent PressEvent;
    public UnityEvent ReleaseEvent;
    private bool _press;
    private Animator _animator;

    private AudioSource _audioSource;
  
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cube>() || other.GetComponent<PlayerController>())
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            _press = true;
            _animator.SetBool("Press", _press);
            PressEvent.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cube>() || other.GetComponent<PlayerController>())
        {
            _press = false;
            _animator.SetBool("Press", _press);
            ReleaseEvent.Invoke();
        }

    }
}
