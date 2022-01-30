using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audioSource.volume = 0;
        StartCoroutine(HighVolume());
    }
    IEnumerator HighVolume()
    {
        for (float i = 0; i < 1;)
        {
            yield return new WaitForSeconds(0.1f);
            i += 0.05f;
            _audioSource.volume = i;
        }
    }

    public void EndMusic()
    {
        StartCoroutine(LowVolume());
    }
    IEnumerator LowVolume()
    {
        StopCoroutine(HighVolume());
        for (float i = 1; i > 0;)
        {
            yield return new WaitForSeconds(0.1f);
            i -= 0.05f;
            _audioSource.volume = i;
        }
    }
}

