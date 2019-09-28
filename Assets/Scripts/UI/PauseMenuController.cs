using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;         // unpause
        menuPanel.SetActive(false); // hide menu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }
    }

    private void HandlePause()
    {
        isPaused = !isPaused;           // toggle pause state

        if (isPaused)
        {
            Time.timeScale = 0;         // pause
            menuPanel.SetActive(true);  // show menu
        }
        else
        {
            Time.timeScale = 1;         // unpause
            menuPanel.SetActive(false); // hide menu
        }
    }

    
}
