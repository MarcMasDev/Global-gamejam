using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public CameraController CameraController;
    public CanvasGroup OptionCanvasGroup;
    private CanvasGroup _canvasGroup;
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
            if (!_show)
            {
                Debug.Log("Show");
                Show();
            }
            else
            {
                Debug.Log("Hide");
                Hide();
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
    }
}
