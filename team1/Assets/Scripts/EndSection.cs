using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndSection : MonoBehaviour
{
    public GameObject sectionEndMenu;
    public ClockTimer timer;

    public void FinishSection()
    {
        sectionEndMenu.SetActive(true);
        Time.timeScale = 0f;
        // This will need to be replaced with the actual playerID, level name, etc.
        OfflineLeaderboard.AddItem(new LeaderboardEntry(0, timer.getScore(), "Test", SceneManager.GetActiveScene().name));
    }
}
