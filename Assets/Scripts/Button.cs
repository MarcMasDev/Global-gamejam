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
    private float _pressingCount;

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
            if (_pressingCount == 0)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                _press = true;
                _animator.SetBool("Press", _press);
                PressEvent.Invoke();
            }
            _pressingCount += 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cube>() || other.GetComponent<PlayerController>())
        {
            if (_pressingCount == 1)
            {
                _press = false;
                _animator.SetBool("Press", _press);
                ReleaseEvent.Invoke();
            }

            _pressingCount -= 1;
        }

    }
}
