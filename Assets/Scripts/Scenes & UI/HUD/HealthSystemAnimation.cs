using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;     // to call the update method once every frame without using monodevelop, removing the need to put this script on an object

/// <summary>
/// Responsible for the animation of the health bar on value change.
/// </summary>

public class HealthSystemAnimation
{
    public event EventHandler OnHealthChanged;      // to subscribe to health changes
    public event EventHandler OnMaxHealthChanged;   // to subscribe to max health changes

    private HealthSystem healthSystem;  // reference to the player's health system
    private bool isAnimating;           // whether or not the bar should be animating
    private float updateTimer;          // 
    private float updateTimerMax;       // 

    int maxHealth;                      // the current max health of this character
    int currentHealth;                  // the current health of this character

    private void Update()
    {
        if (isAnimating)
        {
            // check if it is time to update
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            {
                // time to update
                updateTimer -= updateTimerMax;

                UpdateHealth();
            }
        }
    }

    /// <summary>
    /// Responsible for updating the experience bar.
    /// </summary>

    private void UpdateHealth()
    {
        if (currentHealth < healthSystem.GetCurrentHealth())
        {
            // local current health is less than the target health

            Heal();
        }
        else
        {
            if (currentHealth > healthSystem.GetCurrentHealth())
            {
                // local current health is more than the target health

                TakeDamage();
            }
            else
            {
                // local current health equals the target health

                isAnimating = false;
            }
        }
    }

    /// <summary>
    /// Initialises the health system.
    /// </summary>

    public HealthSystemAnimation(HealthSystem healthSystem)
    {
        SetHealthSystem(healthSystem);
        updateTimerMax = .016f;

        FunctionUpdater.Create(() => Update());     // Code Monkey Utility
    }

    /// <summary>
    /// Sets the current health system and updates the local parameters.
    /// </summary>

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        currentHealth = healthSystem.GetCurrentHealth();
        maxHealth = healthSystem.GetCurrentMaxHealth();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnMaxHealthChanged += HealthSystem_OnMaxHealthChanged;

        Player.instance.levelSystemAnimation.OnLevelChanged += LevelSystemAnimation_OnLevelChanged;
    }

    /// <summary>
    /// Gets called everytime the player health changes.
    /// </summary>

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    /// <summary>
    /// Gets called everytime the player max health changes.
    /// </summary>

    private void HealthSystem_OnMaxHealthChanged(object sender, EventArgs e)
    {
        maxHealth = healthSystem.GetCurrentMaxHealth();

        currentHealth = healthSystem.GetCurrentHealth();

        if (OnMaxHealthChanged != null) OnMaxHealthChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Gets called everytime the player level changes.
    /// </summary>

    private void LevelSystemAnimation_OnLevelChanged(object sender, EventArgs e)
    {
        healthSystem.NewLevel();
    }

    /// <summary>
    /// Responsible for updating the local parameters.
    /// </summary>

    private void Heal()
    {
        currentHealth++;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Responsible for updating the local parameters.
    /// </summary>

    private void TakeDamage()
    {
        currentHealth--;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Returns the current health of this character.
    /// </summary>

    public int GetCurrentHealth() { return currentHealth; }

    /// <summary>
    /// Returns the current max health of this character.
    /// </summary>

    public int GetMaxHealth() { return maxHealth; }

    /// <summary>
    /// Returns the percentage of health remaining. 0 = dead, 1 = maxHealth
    /// </summary>

    public float GetHealthNormalised()
    {
        return (float)currentHealth / healthSystem.GetCurrentMaxHealth();
    }
}
