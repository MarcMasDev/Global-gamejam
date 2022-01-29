using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        
    }
    public void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
