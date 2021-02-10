using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public static bool isGamePause = false;
    public GameObject pauseOptions;
    public void Restart()
    {

        Time.timeScale = 1f;
        isGamePause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public void MainMenu()
    {

        Time.timeScale = 1f;
        isGamePause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {

        pauseOptions.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;


    }

    public void ResumeGame()
    {

        pauseOptions.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isGamePause)
            {
                Restart();
            }

        }


    }
}
