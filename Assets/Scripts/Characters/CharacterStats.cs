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

    public LevelSystem levelSystem;     // the level system of this character

    #endregion

    [Header("Health System")]

    #region Health System

    public int maxHealth = 100;         // the max health points of this character
    public int currentHealth { get; private set; }

    /// <summary>
    /// Returns the percentage of remaining health.
    /// </summary>

    public float GetHealthNormalised()
    {
        return currentHealth / maxHealth;
    }

    /// <summary>
    /// Responsible for dealing damage to this character and sends a bool whether or not the character has died.
    /// </summary>
    /// <param name="damage">the amount of damage to subtract</param>

    public bool TakeDamage(int damage)
    {
        damage -= defence.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Responsible for replenishing health to this character.
    /// </summary>
    /// <param name="healAmount">the amount of health to replenish</param>

    public void Heal(int healAmount)
    {
        if (currentHealth + healAmount > maxHealth)
        {
            // can't have more than max health
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healAmount;
        }
    }

    /// <summary>
    /// Meant to be overriden by the character dying.
    /// </summary>

    public virtual void Die()
    {
        Debug.Log(transform.name + " died...");
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

        // todo load from data instead
        currentHealth = maxHealth;
    }
}
