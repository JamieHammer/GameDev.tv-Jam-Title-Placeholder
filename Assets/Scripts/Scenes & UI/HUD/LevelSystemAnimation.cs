using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;     // to call the update method once every frame without using monodevelop, removing the need to put this script on an object

/// <summary>
/// Responsible for the animation of the experience bar on value change.
/// </summary>

public class LevelSystemAnimation
{
    public event EventHandler OnExperienceChanged;      // to subscribe to experience changes
    public event EventHandler OnLevelChanged;           // to subscribe to level changes

    private LevelSystem levelSystem;    // reference to the player's level system
    private bool isAnimating;           // whether or not the bar should be animating
    private float updateTimer;          // 
    private float updateTimerMax;       // 

    int level;                          // the current level of this character
    int experience;                     // the amount of experience gained already

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

                UpdateAddExperience();
            }
        }
    }

    /// <summary>
    /// Responsible for updating the experience bar.
    /// </summary>

    private void UpdateAddExperience()
    {
        if (level < levelSystem.GetLevelNumber())
        {
            // local level is below target level

            AddExperience();
        }
        else
        {
            // local level equals the target level

            if (experience < levelSystem.GetExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }

    /// <summary>
    /// Initialises the levelsystem.
    /// </summary>

    public LevelSystemAnimation(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = .016f;

        FunctionUpdater.Create(() => Update());     // Code Monkey Utility
    }

    /// <summary>
    /// Sets the current level system and updates the local parameters.
    /// </summary>

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    /// <summary>
    /// Gets called everytime the player level changes.
    /// </summary>

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    /// <summary>
    /// Gets called everytime the player experience changes.
    /// </summary>

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    /// <summary>
    /// Responsible for updating the local parameters.
    /// </summary>

    private void AddExperience()
    {
        experience++;
        if (experience >= levelSystem.GetExperienceToNextLevel(level))
        {
            level++;
            experience = 0;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Returns the current level number of this character.
    /// </summary>

    public int GetLevelNumber() { return level; }

    /// <summary>
    /// Returns the current experience of this character.
    /// </summary>

    public int GetExperience() { return experience; }

    /// <summary>
    /// Returns the experience needed for the next level.
    /// </summary>
    /// <param name="levelIndex">the index of the current level</param>

    public int GetExperienceToNextLevel(int levelIndex)
    {
        return levelSystem.GetExperienceToNextLevel(levelIndex) * 10;
    }

    /// <summary>
    /// Returns the percentage until next level is reached.
    /// </summary>

    public float GetExperienceNormalised()
    {
        return (float)experience / levelSystem.GetExperienceToNextLevel(level);
    }
}
