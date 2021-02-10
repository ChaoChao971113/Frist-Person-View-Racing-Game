[System.Serializable]
public class LeaderboardEntry
{
    public int playerID;
    public float time;
    public string name;
    public string level;

    public LeaderboardEntry()
    {
        playerID = -1;
        time = -1.0f;
        name = "";
        level = "";
    }

    public LeaderboardEntry(int playerID, float time, string name, string level)
    {
        this.playerID = playerID;
        this.time = time;
        this.name = name;
        this.level = level;
    }

    public override string ToString()
    {
        return "PlayerID: " + playerID + ", Time: " + time + ", Name: " + name + ", Level: " + level;
    }
}
