using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject QuitButton;
    public GameObject RestartButton;
    public TextMeshProUGUI Paused;
    public bool MenuUp = false;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        QuitButton.SetActive(false);
        RestartButton.SetActive(false);
        MenuUp = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        QuitButton.SetActive(true);
        RestartButton.SetActive(true);
        MenuUp = true;
        Time.timeScale = 0f;
        Debug.Log("bruh");
        GameIsPaused = true;
        Paused.text = "Paused";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void sceneSwitch()
    {
        SceneManager.LoadScene(0);
        Resume();
    }
}
