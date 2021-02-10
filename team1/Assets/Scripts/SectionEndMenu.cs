using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SectionEndMenu : MonoBehaviour
{
    public ClockTimer timer;
    public Text header;
    public Text timeText;
    public Button nextButton;

    private void OnEnable()
    {
        timer.StopTimer();
        nextButton.enabled = NextButtonEnabled();
        header.text = GameManager.Instance.zoneMode ? "Zone Complete!" : "Level Complete!";
        timeText.text = TimeSpan.FromSeconds(timer.getScore()).ToString(@"mm\:ss\.fff");
    }

    public void NextSection()
    {
        if (GameManager.Instance.zoneMode)
        {
            GameManager.Instance.checkPoint++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Load the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Time.timeScale = 1f;
    }

    private bool NextButtonEnabled()
    {
        //Either we are in zone mode and there are more checkpoints, or we are in level mode and there are more levels
        return (GameManager.Instance.zoneMode && !LevelManager.Instance.IsLastCheckpoint()) || (!GameManager.Instance.zoneMode && SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings);
    }
}
