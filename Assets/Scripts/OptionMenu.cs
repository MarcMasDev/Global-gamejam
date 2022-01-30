using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer TheMixer;
    public Slider musicSlider, sfxSlider;
    public TMP_Dropdown ResolutionDropdown;

    public TMP_Text FullscreenText;

    public Image _button;

    public Color FullscreenOn;

    public Color FullscreenOff;

    private bool _fullscreen = true;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        SetMusicVolume();
        SetSFXVolume();
        //SetMasterVolume();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        _fullscreen = (_fullscreen) ? false : true;
        if (_fullscreen)
        {
            _button.color = FullscreenOn;
            FullscreenText.text = "Fullscreen: ON";
        }
        else
        {
            _button.color = FullscreenOff;
            FullscreenText.text = "Fullscreen: OFF";
        }
        Screen.fullScreen = _fullscreen;
    }


    ///



    public void SetMasterVolume()
    {
        TheMixer.SetFloat("MasterVol", 0);
    }
    public void SetMusicVolume()
    {

        TheMixer.SetFloat("MusicVol", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetSFXVolume()
    {

        TheMixer.SetFloat("SFXVol", Mathf.Log10((sfxSlider.value) * 20));
    }
}
