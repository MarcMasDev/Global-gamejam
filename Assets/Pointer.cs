using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public RectTransform Minus;
    public RectTransform Plus;

    private void Start()
    {
        Hide();
    }
    public void ShowMinus()
    {
        Minus.gameObject.SetActive(true);
        Plus.gameObject.SetActive(false);
    }
    public void ShowPlus()
    {
        Minus.gameObject.SetActive(false);
        Plus.gameObject.SetActive(true);
    }
    public void Hide()
    {
        Minus.gameObject.SetActive(false);
        Plus.gameObject.SetActive(false);
    }
}
