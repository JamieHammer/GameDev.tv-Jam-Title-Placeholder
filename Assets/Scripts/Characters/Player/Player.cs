using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To manage everything related to the player.
/// </summary>

public class Player : CharacterStats
{
    #region Singleton
    public static Player instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] private PlayerWindow playerWindow;

    LevelSystem levelSystem;            // reference to the level system of this player
    Transform playerTransform;          // reference to the player transform

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            defence.AddModifier(newItem.GetArmourModifier());
            attack.AddModifier(newItem.GetDamageModifier());
        }

        if (oldItem != null)
        {
            defence.RemoveModifier(oldItem.GetArmourModifier());
            attack.RemoveModifier(oldItem.GetDamageModifier());
        }
    }

    /// <summary>
    /// Responsible for adding the experience and act accordingly.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void AddExperience(int amount)
    {
        Debug.Log("Add " + amount + " of XP");

        levelSystem.AddExperience(amount);
    }

    /// <summary>
    /// Responsible for creating a new player.
    /// </summary>

    public void NewPlayer()
    {
        levelSystem = new LevelSystem(0, 0);

        levelSystemAnimation = new LevelSystemAnimation(levelSystem);
        SetLevelSystemAnimation(levelSystemAnimation);

        UpdateUI();
    }

    /// <summary>
    /// Loads the player data from the save file.
    /// </summary>
    /// <param name="data"></param>

    public void LoadPlayer(PlayerData data)
    {
        if (data != null)
        {
            // INFO

            characterName = data.name;

            // LEVEL

            int level = data.level;
            int experience = data.experience;

            levelSystem = new LevelSystem(level, experience);

            levelSystemAnimation = new LevelSystemAnimation(levelSystem);
            SetLevelSystemAnimation(levelSystemAnimation);



            string time = "" + System.DateTime.Now;

            Debug.Log(time);

            

            // HEALTH

            maxHealth = data.maxHealth;
            LoadCurrentHealth(data.currentHealth);

            // ABILITIES

            strength = data.strength;
            stamina = data.stamina;
            intelligence = data.intelligence;
            dexterity = data.dexterity;
            charisma = data.charisma;
            luck = data.luck;
            attack = data.attack;
            defence = data.defence;

            // POSITION

            float x = data.position[0];
            float y = data.position[1];
            float z = data.position[2];

            playerTransform.position.Set(x, y, z);
        }
        else
        {
            Debug.Log("Meuh");
        }
    }

    public void UpdateUI()
    {
        playerWindow.SetLevelSystemAnimation(levelSystemAnimation);

        // todo
    }
}
