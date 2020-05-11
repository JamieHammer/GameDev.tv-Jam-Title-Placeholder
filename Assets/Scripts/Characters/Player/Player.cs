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
    HealthSystem healthSystem;                  // reference to the health system of this player
    Interactable interactable;                  // reference to the item or character that is interactable

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Interact();
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            defence.AddModifier(newItem.GetDefenceModifier());
            attack.AddModifier(newItem.GetAttackModifier());
        }

        if (oldItem != null)
        {
            defence.RemoveModifier(oldItem.GetDefenceModifier());
            attack.RemoveModifier(oldItem.GetAttackModifier());
        }
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

        // LEVEL SYSTEM

        levelSystem = new LevelSystem(1, 0);
        levelSystemAnimation = new LevelSystemAnimation(levelSystem);

        playerWindow.SetLevelSystemAnimation(levelSystemAnimation);
        SetLevelSystemAnimation(levelSystemAnimation);

        // HEALTH SYSTEM

        healthSystem = new HealthSystem(levelSystem.GetLevelNumber(), 0);
        healthSystemAnimation = new HealthSystemAnimation(healthSystem);

        playerWindow.SetHealthSystemAnimation(healthSystemAnimation);
        SetHealthSystemAnimation(healthSystemAnimation);

        // UPDATE UI

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

            // HEALTH

            int currentHealth = data.currentHealth;

            healthSystem = new HealthSystem(level, currentHealth);

            healthSystemAnimation = new HealthSystemAnimation(healthSystem);
            SetHealthSystemAnimation(healthSystemAnimation);
            playerWindow.SetHealthSystemAnimation(healthSystemAnimation);

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

            playerTransform.position = new Vector2(x, y);

            CameraReset.instance.ResetCamera();
        }
        else
        {
            Debug.LogError("Save file not found...");
        }
    }

    /// <summary>
    /// Responsible for adding the experience and act accordingly.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void TakeDamage(int amount)
    {
        Debug.Log("Showing hurt animation.");

        bool isDead = healthSystem.TakeDamage(amount, defence.GetValue());
        playerWindow.SetHP();

        if (isDead)
        {
            //characterController.Die();
            Debug.Log("Showing death animation.");
        }
    }

    /// <summary>
    /// Responsible for adding the health to the player.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void Heal(int amount)
    {
        healthSystem.AddHealth(amount);
        playerWindow.SetHP();
    }

    /// <summary>
    /// Responsible for adding the experience and act accordingly.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void AddExperience(int amount)
    {
        levelSystem.AddExperience(amount);
        playerWindow.SetExperience();
    }

    /// <summary>
    /// Responsible for updating the player window UI.
    /// </summary>

    public void UpdateUI()
    {
        playerWindow.SetPlayer();
    }

    /// <summary>
    /// Makes the player able to interact with interactables.
    /// </summary>

    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    /// <summary>
    /// Handles trigger events for interactables.
    /// </summary>

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactable = collision.GetComponent<Interactable>();
        }
    }

    /// <summary>
    /// Handles trigger events for interactables.
    /// </summary>

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (interactable != null)
            {
                interactable.StopInteract();
                interactable = null;
            }
        }
    }
}
