//SaveData Class
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<Player> players;

    public SaveData()
    {
        players = new List<Player>();
    }

}
