using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaAnim : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Show(bool showornot)
    {
        print("A");
        anim.SetBool("Show", showornot);
    }
}
