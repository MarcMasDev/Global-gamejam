using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerEndScene : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private float maxTimer = 25f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        print(timer);

        if (timer >= maxTimer)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
