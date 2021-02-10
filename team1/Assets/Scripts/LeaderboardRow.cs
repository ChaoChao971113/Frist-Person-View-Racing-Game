using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LeaderboardRow : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI timeText;

    public void SetFields(int rank, LeaderboardEntry entry)
    {
        rankText.text = rank.ToString();
        stageText.text = entry.level;
        playerText.text = entry.name;
        timeText.text = TimeSpan.FromSeconds(entry.time).ToString(@"mm\:ss\.fff");
    }
}
