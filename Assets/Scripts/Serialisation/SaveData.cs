using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SaveFile

/// <summary>
/// A save file to hold game data.
/// </summary>

[Serializable]
public class SaveData
{
    public PlayerData playerData { get; set; }              // the player data

    public List<ChestData> chestData { get; set; }          // a list of chest data

    /// <summary>
    /// The save data file construct.
    /// </summary>

    public SaveData()
    {
        chestData = new List<ChestData>();
    }
}

#endregion

#region Data Holders

/// <summary>
/// Helps with saving player data.
/// </summary>

[Serializable]
public class PlayerData
{
    [Header("Info")]

    public string name;                 // name of the player
    // date and time of save

    [Header("Level")]

    public int level;                   // level of the player
    public int experience;              // the amount of experience gained already

    [Header("Health")]

    public int currentHealth;           // remaining health of the player

    [Header("Abilities")]

    public Stat strength;               // the base strength
    public Stat stamina;                // the base stamina
    public Stat intelligence;           // the base intelligence
    public Stat dexterity;              // the base dexterity
    public Stat charisma;               // the base charisma
    public Stat luck;                   // the base luck
    public Stat attack;                 // the base offensive ability for dealing more damage
    public Stat defence;                // the base defensive ability for avoiding taking damage

    [Header("Position")]

    public float[] position;            // an array of floats corresponding to a player position

    /// <summary>
    /// Player data constructor.
    /// </summary>

    public PlayerData(Player player)
    {
        // INFO

        name = player.characterName;

        // LEVEL

        level = player.levelSystemAnimation.GetLevelNumber();
        experience = player.levelSystemAnimation.GetExperience();

        // HEALTH

        currentHealth = player.healthSystemAnimation.GetCurrentHealth();

        // ABILITIES

        strength = player.strength;
        stamina = player.stamina;
        intelligence = player.intelligence;
        dexterity = player.dexterity;
        charisma = player.charisma;
        luck = player.luck;
        attack = player.attack;
        defence = player.defence;

        // POSITION

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}

/// <summary>
/// Helps with saving item data.
/// </summary>

[Serializable]
public class ItemData
{
    [Header("Info")]

    public string name;                 // name of the item

    public int stackCount;              // the amount of items held

    public int slotIndex;               // the slot index of the item

    /// <summary>
    /// Item data constructor.
    /// </summary>

    public ItemData(Item item)
    {
        // INFO

        name = item.GetName();
    }
}

/// <summary>
/// Helps with saving chest data.
/// </summary>

[Serializable]
public class ChestData
{
    [Header("Info")]

    public string name;                 // name of the item

    public List<Item> items;            // a list of items contained in the chest

    /// <summary>
    /// Item data constructor.
    /// </summary>

    public ChestData(Chest chest)
    {
        // INFO

        name = "";
    }
}

/// <summary>
/// Helps with saving quest data.
/// </summary>

[Serializable]
public class QuestData
{
    [Header("Info")]

    public string name;                 // name of the quest

    /// <summary>
    /// Item data constructor.
    /// </summary>

    public QuestData()
    {
        // INFO

        name = "";
    }
}

/// <summary>
/// Helps with saving save slot data.
/// </summary>

[Serializable]
public class SaveSlotData
{
    [Header("Info")]

    public string playerName;                   // name of the item

    public string playerLevel;                  // the level of the player

    public string location;                     // the location of the save

    public string chapter;                      // the reached chapter of the main story

    public string playTime;                     // the number of hours and minutes played

    public string lastPlayed;                   // the date and time of the last play session

    /// <summary>
    /// Item data constructor.
    /// </summary>

    public SaveSlotData()
    {
        // INFO

        playerName = "";

        playerLevel = "";

        location = "";

        chapter = "";

        playTime = "";

        lastPlayed = "";
    }
}

#endregion