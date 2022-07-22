//Player Class

[System.Serializable]
public class Player {

    public int playerID;
    public string playerName;
    public int playerScore;

    public Player()
    {
        this.playerID = -1;
        this.playerName = "NA";
        this.playerScore = 0;
    }

    public Player(int id, string name)
    {
        this.playerID = id;
        this.playerName = name;
        this.playerScore = 0;
    }

}
