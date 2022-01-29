using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    PlayerController _playerController;
    CameraController _cameraController;
    RejectArea _rejectArea;

    private void Awake()
    {
        gameManager = this;
    }
    public static GameManager GetManager() { return gameManager; }
    

    //SET
    public void SetCamera(CameraController cam)
    {
        _cameraController = cam;
    }
    public void SetPlayer(PlayerController player)
    {
        _playerController = player;
    }
    public void SetRejectarea(RejectArea reject)
    {
        _rejectArea = reject;
    }
    //GET
    public RejectArea GetRejectArea() { return _rejectArea; }
    public CameraController GetCamera() { return _cameraController; }
    public PlayerController GetPlayer() { return _playerController; }
}
