using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public CameraController CameraController;
    public CanvasGroup OptionCanvasGroup;
    private CanvasGroup _canvasGroup;
    public GameObject GlobalVolume;
    public bool _escDisabled;
    private bool _show;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_escDisabled)
            {
                if (!_show)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }
    }
    public void Hide() 
    {
        CameraController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _show = false;
        GlobalVolume.SetActive(false);
    }
    public void Show() 
    {
        CameraController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _show = true;
        GlobalVolume.SetActive(true);
    }
    public void ShowOption()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _show = false;
        OptionCanvasGroup.alpha = 1;
        OptionCanvasGroup.interactable = true;
        OptionCanvasGroup.blocksRaycasts = true;
        _escDisabled = true;
    }

    public void HideOption()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _show = true;
        OptionCanvasGroup.alpha = 0;
        OptionCanvasGroup.interactable = false;
        OptionCanvasGroup.blocksRaycasts = false;
        _escDisabled = false;
    }
}
