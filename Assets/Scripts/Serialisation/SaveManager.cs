using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Manages the save system.
/// </summary>

public class SaveManager : MonoBehaviour
{
    #region Singleton

    public static SaveManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    public static string path;

    Player player;              // reference to the player
    Chest[] chests;             // reference to all chests

    private void Start()
    {
        SaveManager.path = Application.persistentDataPath + "/SaveTest.dat";

        player = Player.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadGame();
        }
    }

    /// <summary>
    /// Responsible for saving the game.
    /// </summary>

    private void SaveGame()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Create);

            SaveData data = new SaveData();

            SavePlayer(data);

            formatter.Serialize(stream, data);
            stream.Close();

            Debug.Log("Game has been saved!");
        }
        catch (System.Exception)
        {
            Debug.Log("Something went wrong, trying to save the game...");
        }
    }

    /// <summary>
    /// Responsible for loading the game file.
    /// </summary>

    private void LoadGame()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

            SaveData data = (SaveData)formatter.Deserialize(stream);

            stream.Close();

            LoadPlayer(data);

            Debug.Log("Game has been loaded!");
        }
        catch (System.Exception)
        {
            Debug.Log("Something went wrong, trying to load the game file...");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>

    public void SavePlayer(SaveData data)
    {
        data.playerData = new PlayerData(player);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>

    public void LoadPlayer(SaveData data)
    {
        player.LoadPlayer(data.playerData);
        player.UpdateUI();
    }
}
