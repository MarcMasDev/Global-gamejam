using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public MusicController Music;
    public void Play()
    {
        Music.EndMusic();
        StartCoroutine(DelayPlay());
    }

    IEnumerator DelayPlay()
    {
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public void BackMenu()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;

    }
    public void Exit()
    {
        Application.Quit();
    }
}
