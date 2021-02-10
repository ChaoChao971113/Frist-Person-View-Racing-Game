using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class OfflineLeaderboard
{
    private static string path = Application.persistentDataPath + "/leaderboard.pgl";
    private static List<LeaderboardEntry> leaderboard;

    static OfflineLeaderboard()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            leaderboard = formatter.Deserialize(stream) as List<LeaderboardEntry>;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("No local leaderboard found");
            leaderboard = new List<LeaderboardEntry>();
        }
    }

    public static void AddItem(LeaderboardEntry entry)
    {
        leaderboard.Add(entry);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, leaderboard);
        stream.Close();
    }

    public static List<LeaderboardEntry> GetLeaderboard()
    {
        return leaderboard.OrderBy(o => o.level).ThenBy(o => o.time).ToList();
    }

    public static List<LeaderboardEntry> GetLeaderboard(string level)
    {
        return leaderboard.Where(o => o.level == level).OrderBy(o => o.time).ToList();
    }

    public static List<LeaderboardEntry> GetLeaderboard(int playerID)
    {
        return leaderboard.Where(o => o.playerID == playerID).OrderBy(o => o.time).ToList();
    }
}
