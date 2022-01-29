using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent PressEvent;
    public UnityEvent ReleaseEvent;
    private bool _press;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _press = true;
        _animator.SetBool("Press", _press);
        PressEvent.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        _press = false;
        _animator.SetBool("Press", _press);
        ReleaseEvent.Invoke();
    }
}
