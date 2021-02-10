using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    public GameObject previousMenu;

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void setZoneMode(bool zoneMode)
    {
        GameManager.Instance.zoneMode = zoneMode;
    }

    public void setCheckpoint(int checkpoint)
    {
        GameManager.Instance.SetCheckpoint(checkpoint);
    }

    public void ExitGame()
    {        
        Application.Quit();
    }

    public void SetGear(GameObject newGear)
    {
        GameManager.Instance.SetGear(newGear);
    }

    public void SetGear2(GameObject newGear)
    {
        GameManager.Instance.SetGear2(newGear);
    }

    public void SetPrevious(GameObject previous) // Save the last menu we were at
    {
        previousMenu = previous;
    }

    public void Back() // Return to the last menu
    {
        previousMenu.SetActive(true);
    }

}
