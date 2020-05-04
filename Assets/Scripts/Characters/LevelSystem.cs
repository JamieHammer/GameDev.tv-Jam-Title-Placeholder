using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the level system.
/// </summary>

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;      // to subscribe to experience changes
    public event EventHandler OnLevelChanged;           // to subscribe to level changes

    int level;                          // the current level of this character
    int experience;                     // the amount of experience gained already
    int experienceToNextLvl;            // the amount of experience needed to reach next level

    /// <summary>
    /// Constructs a level system from the class.
    /// </summary>

    public LevelSystem ()
    {
        level = 0;
        experience = 0;
        experienceToNextLvl = 100;
    }

    /// <summary>
    /// Responsible for adding the experience and act accordingly.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLvl)
        {
            // enough experience to level up

            level++;
            experience -= experienceToNextLvl;
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
        return experienceToNextLvl * 10;
    }

    /// <summary>
    /// Returns the percentage until next level is reached.
    /// </summary>

    public float GetExperienceNormalised()
    {
        return (float)experience / experienceToNextLvl;
    }
}
