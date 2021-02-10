using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardMenu : MonoBehaviour
{
    public LeaderboardRow leaderboardRow;
    public Transform leaderboardContent;
    public int rowHeight = 35;
    public TMP_Dropdown levelDropdown;
    public TextMeshProUGUI stageLabel;
    private List<LeaderboardEntry> leaderboard;
    private List<LeaderboardRow> rows;

    void OnEnable()
    {
        leaderboard = OfflineLeaderboard.GetLeaderboard(stageLabel.text);
        if (rows != null)
            ClearLeaderboard();
        else
            rows = new List<LeaderboardRow>();
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        int rank = 1;
        foreach (LeaderboardEntry entry in leaderboard)
        {
            LeaderboardRow row = Instantiate(leaderboardRow, leaderboardContent);
            rows.Add(row);
            row.SetFields(rank, entry);
            rank++;
        }
    }

    private void ClearLeaderboard()
    {
        foreach (LeaderboardRow row in rows)
        {
            Destroy(row.gameObject);
        }
        rows = new List<LeaderboardRow>();
    }

    public void ChangeStage()
    {
        ClearLeaderboard();
        leaderboard = OfflineLeaderboard.GetLeaderboard(stageLabel.text);
        DisplayLeaderboard();
    }
}
