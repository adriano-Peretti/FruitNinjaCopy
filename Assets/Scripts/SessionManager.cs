using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SessionManager {

    private const string FILENAME = "leaderboard.json";

    private static Player currentPlayer;
    private static SaveData save;
    private static bool isReady = false;

    public static void Initialize()
    {
        if (isReady)
            return;

        save = new SaveData();
        LoadPlayerData();
    }

    /// <summary>
    /// Prepare session to add it after
    /// </summary>
    /// <param name="playerName"></param>
    public static void CreateSession(string playerName)
    {
        int id = 0;
        if (save.players != null)
        {
            id = save.players.Count + 1;
        }
        currentPlayer = new Player(id, playerName);
    }

    public static void AddCurrentPlayer()
    {
        Debug.Log("Jogador adicionado a lista: " + currentPlayer.playerName);
        save.players.Add(currentPlayer);
        SavePlayerData();
    }

    /// <summary>
    /// Creates a new player and add it to the .JSON file.
    /// </summary>
    /// <param name="playerName"></param>
    public static void StartNewSession(string playerName)
    {
        LoadPlayerData();
        int id = 0;
        if(save.players != null)
        {
            id = save.players.Count + 1;
        }
        currentPlayer = new Player(id, playerName);
        AddNewPlayer(currentPlayer);
    }

    private static void AddNewPlayer(Player p)
    {
        Debug.Log("Jogador adicionado a lista: " + p.playerName);
        save.players.Add(p);
        SavePlayerData();
    }

    /// <summary>
    /// Update the current session player's score and save the data
    /// </summary>
    /// <param name="score"></param>
    public static void UpdatePlayerScore(int score)
    {
        currentPlayer.playerScore = score;
        SavePlayerData();
    }

    public static Player GetSessionPlayer()
    {
        return currentPlayer;
    }

    /// <summary>
    /// Loads all players in the .JSON file.
    /// </summary>
    public static void LoadPlayerData()
    {
        if (isReady)
            return;

        save.players = new List<Player>();

        string path = Application.streamingAssetsPath + "/SaveData/" + FILENAME;
        string jsonString = File.ReadAllText(path);

        if (jsonString != null || jsonString != "")
        {
            SaveData temp = JsonUtility.FromJson<SaveData>(jsonString);
            Debug.Log("Dados carregados:\n" + jsonString);
            if(temp != null)
            {
                save = temp;
                Debug.Log("Dados carregados com sucesso");
            }
            else
            {
                Debug.Log("Erro ao carregar os dados: Nenhum elemento na lista");
            }
        }
        else
        {          
            Debug.Log("Erro ao carregar os dados:\n" + jsonString);
        }

        if(!isReady)
        {
            isReady = true;
        }
    }

    public static void ReloadPlayerData()
    {
        isReady = false;
        LoadPlayerData();
    }

    /// <summary>
    /// Saves the players array into the .JSON file.
    /// </summary>
    public static void SavePlayerData()
    {
        string path = Application.streamingAssetsPath + "/SaveData/" + FILENAME;
        string jsonString = JsonUtility.ToJson(save, true);
        File.WriteAllText(path, jsonString);
        Debug.Log("Dados salvos em:" + path + "\n" + jsonString);
    }

    public static bool Ready()
    {
        return isReady;
    }

    public static Player[] GetTopThree()
    {
        Player[] topThree = { new Player(), new Player(), new Player() };

        for (int i = 0; i < save.players.Count; i++)
        {
            Player temp = save.players[i];
            if (temp.playerScore > topThree[2].playerScore)
            {
                topThree[2] = temp;

                if (temp.playerScore > topThree[1].playerScore)
                {
                    topThree[2] = topThree[1];
                    topThree[1] = temp;

                    if (temp.playerScore > topThree[0].playerScore)
                    {
                        topThree[1] = topThree[0];
                        topThree[0] = temp;
                    }
                }
            }
        }
        return topThree;
    }

}
