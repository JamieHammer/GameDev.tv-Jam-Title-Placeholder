using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Responsible for creating, saving and loading save files.
/// </summary>

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/player.save";

    public static void SavePlayer(PlayerStats player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }
}
