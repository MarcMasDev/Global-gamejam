using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    public float Target;
    public FadeCollider FadeCollider;
    private float _count;

    private void Update()
    {
        if (_count >= Target)
        {
            FadeCollider.OpenPortal();
        }
    }
    
    public void Add()
    {
        _count += 1;
    }
    public void Resta()
    {
        _count -= 1;
    }
}
