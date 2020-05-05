using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the character health system.
/// </summary>

public class HealthSystem
{
    public event EventHandler OnHealthChanged;      // to subscribe to health changes
    public event EventHandler OnMaxHealthChanged;   // to subscribe to max health changes

    int maxHealth;                                  // the current max health of this character
    int currentHealth;                              // the current health of this character

    int basehealth = 30;                            // the base amount of health of this character

    /// <summary>
    /// Constructs a health system from the class.
    /// </summary>

    public HealthSystem (int level, int current)
    {
        maxHealth = GetMaxHealth(level);

        if (current == 0)
        {
            // the player cannot save while dead, so if current health = 0, it is a new game

            currentHealth = maxHealth;
        }
        else
        {
            // else the health system being created is from a save file

            currentHealth = current;
        }
    }

    /// <summary>
    /// Responsible for healing the character.
    /// </summary>
    /// <param name="amount">Amount of health to add</param>

    public void AddHealth(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            // can't have more than max health
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Responsible for dealing damage to the character.
    /// </summary>
    /// <param name="amount">Amount of health to subtract</param>

    public bool TakeDamage(int amount, int defenceStat)
    {
        if (amount > defenceStat)
        {
            amount -= defenceStat;
            amount = Mathf.Clamp(amount, 0, int.MaxValue);
        }
        else
        {
            amount = 1;
        }

        currentHealth -= amount;

        if (currentHealth < 0)
        {
            // we don't want to have negative health.
            currentHealth = 0;
        }

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

        if (currentHealth == 0)
        {
            Die();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Increases the max health when the character level up.
    /// </summary>

    public void NewLevel()
    {
        int oldMax = maxHealth;

        maxHealth = GetMaxHealth(Player.instance.levelSystemAnimation.GetLevelNumber());

        int difference = maxHealth - oldMax;

        currentHealth += difference;

        if (OnMaxHealthChanged != null) OnMaxHealthChanged(this, EventArgs.Empty);

        Debug.Log("New level! Max health increased to: " + maxHealth);
    }

    /// <summary>
    /// Returns the current health of this character.
    /// </summary>

    public int GetCurrentHealth() { return currentHealth; }

    /// <summary>
    /// Returns the current max health of this character.
    /// </summary>

    public int GetCurrentMaxHealth() { return maxHealth; }

    /// <summary>
    /// Returns the current max health of this character.
    /// </summary>
    /// <param name="level">the index of the current level</param>

    public int GetMaxHealth(int level)
    {
        return Mathf.FloorToInt(basehealth) * level / 100 + level + basehealth;
    }

    /// <summary>
    /// Returns the percentage until next level is reached.
    /// </summary>

    public float GetHealthNormalised(int level)
    {
        return (float)currentHealth / GetMaxHealth(level);
    }

    /// <summary>
    /// Meant to be overriden by the character dying.
    /// </summary>

    public virtual void Die()
    {
        // overwriting the method makes it possible for different effects
        // depending on who dies
    }
}
