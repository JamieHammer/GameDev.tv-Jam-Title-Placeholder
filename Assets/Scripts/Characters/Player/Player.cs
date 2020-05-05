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

    PlayerWindow playerWindow;                  // reference to the player HUD window
    LevelSystem levelSystem;                    // reference to the level system of this player

    Transform playerTransform;                  // reference to the player transform
    CharacterController2D characterController;  // reference to the character controller component on the player

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController2D>();
        playerWindow = PlayerWindow.instance;

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
        Debug.Log(levelSystem.GetExperience());

        levelSystem.AddExperience(amount);
        playerWindow.SetExperience();

        Debug.Log("add: " + amount);
        Debug.Log(levelSystem.GetExperience());
    }

    /// <summary>
    /// Responsible for creating a new player.
    /// </summary>

    public void NewPlayer()
    {
        if (playerWindow == null)
        {
            playerWindow = PlayerWindow.instance;
        }

        levelSystem = new LevelSystem(1, 0);

        LoadCurrentHealth(maxHealth);

        levelSystemAnimation = new LevelSystemAnimation(levelSystem);
        playerWindow.SetLevelSystemAnimation(levelSystemAnimation);
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
            playerWindow.SetLevelSystemAnimation(levelSystemAnimation);



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

    // Debug

    public void TakePlayerDamage(int damage)
    {


        bool isDead = TakeDamage(damage);
        playerWindow.SetHP();

        Debug.Log("Player has taken " + damage + " in damage" + "\n" +
            "Remaining health: " + currentHealth);

        if (isDead)
        {
            //characterController.Die();
            Debug.Log("Showing death animation.");
        }
    }

    public void UpdateUI()
    {
        playerWindow.SetPlayer();        
    }
}
