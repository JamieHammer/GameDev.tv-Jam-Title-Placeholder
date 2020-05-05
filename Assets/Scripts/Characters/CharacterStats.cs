using UnityEngine;

/// <summary>
/// Stats for any character.
/// </summary>

public class CharacterStats : MonoBehaviour
{
    [Header("Character Info")]

    public string characterName;        // the name of this character

    [Header("Level System")]

    #region Level System

    public LevelSystemAnimation levelSystemAnimation;     // the level system of this character

    /// <summary>
    /// Responsible for setting up the level system of this character.
    /// </summary>
    /// <param name="levelSystemAnimation">the level system to set as current</param>

    public void SetLevelSystemAnimation(LevelSystemAnimation levelSystemAnimation)
    {
        this.levelSystemAnimation = levelSystemAnimation;

        // subscribe to the on level changed, callback
        levelSystemAnimation.OnLevelChanged += LevelSystemAnimation_OnLevelChanged;
    }

    /// <summary>
    /// Subscribe to the level system's on level changed, callback.
    /// </summary>

    private void LevelSystemAnimation_OnLevelChanged(object sender, System.EventArgs e)
    {
        // todo implement level up

        // sfx

        // particle effect
    }

    #endregion

    [Header("Health System")]

    #region  Health System

    public HealthSystemAnimation healthSystemAnimation;     // the health system of this character

    /// <summary>
    /// Responsible for setting up the health system of this character.
    /// </summary>
    /// <param name="healthSystemAnimation">the health system to set as current</param>

    public void SetHealthSystemAnimation(HealthSystemAnimation healthSystemAnimation)
    {
        this.healthSystemAnimation = healthSystemAnimation;

        // subscribe to the on health changed, callback
        healthSystemAnimation.OnHealthChanged += HealthSystemAnimation_OnHealthChanged;
    }

    /// <summary>
    /// Subscribe to the health system's on health changed, callback.
    /// </summary>

    private void HealthSystemAnimation_OnHealthChanged(object sender, System.EventArgs e)
    {
        // todo implement affordance when taking or dealing damage
    }

    #endregion

    [Header("Abilities System")]

    #region Abilities System

    public Stat strength;               // the base strength
    public Stat stamina;                // the base stamina
    public Stat intelligence;           // the base intelligence
    public Stat dexterity;              // the base dexterity
    public Stat charisma;               // the base charisma
    public Stat luck;                   // the base luck
    public Stat attack;                 // the base offensive ability for dealing more damage
    public Stat defence;                // the base defensive ability for avoiding taking damage

    #endregion

    private void Awake()
    {
        SetupCharacter();
    }

    void SetupCharacter()
    {
        strength.SetName("Strength");
        stamina.SetName("Stamina");
        intelligence.SetName("Intelligence");
        dexterity.SetName("Dexterity");
        charisma.SetName("Charisma");
        luck.SetName("Luck");
        attack.SetName("Attack");
        defence.SetName("Defence");
    }
}
