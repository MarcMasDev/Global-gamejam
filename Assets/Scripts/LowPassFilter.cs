using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilter : MonoBehaviour
{
    private AudioLowPassFilter _passFilter;
    private float cutOff = 5000;
    private float resonance = 1;
    private void Start()
    {
        _passFilter = GetComponent<AudioLowPassFilter>();
    }


    public void ModifiyPassFilter()
    {
        StartCoroutine(DelayModify());
    }

    public void ResetPassFilter()
    {
        StartCoroutine(DelayReset());
    }

    private IEnumerator DelayModify()
    {
        StopCoroutine(DelayReset());
        _passFilter.lowpassResonanceQ = 3f;
        for (float i = cutOff; i > 580; )
        {
            yield return new WaitForSeconds(0.1f);
            i -= 500;
            if (i < 580)
                i = 580;
            _passFilter.cutoffFrequency = i;
        }
    }


    public IEnumerator DelayReset()
    {
        StopCoroutine(DelayModify());
        _passFilter.lowpassResonanceQ = resonance;
        for (float i = 580; i < 5000;)
        {
            yield return new WaitForSeconds(0.1f);
            i += 500;
            if (i > 5000)
                i = 5000;
            _passFilter.cutoffFrequency = i;
        }
    }
}

