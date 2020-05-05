using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the character level system.
/// </summary>

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;      // to subscribe to experience changes
    public event EventHandler OnLevelChanged;           // to subscribe to level changes

    int level;                          // the current level of this character
    int experience;                     // the amount of experience gained already

    int baseExp = 100;                  // the base amount of xp needed for the next level

    /// <summary>
    /// Constructs a level system from the class.
    /// </summary>

    public LevelSystem (int levelNew, int experienceNew)
    {
        level = levelNew;
        experience = experienceNew;
    }

    /// <summary>
    /// Responsible for adding the experience and act accordingly.
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= GetExperienceToNextLevel(level))
        {
            // enough experience to level up

            level++;
            experience -= GetExperienceToNextLevel(level - 1);
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
        return levelIndex * 10;
    }

    /// <summary>
    /// Returns the percentage until next level is reached.
    /// </summary>

    public float GetExperienceNormalised()
    {
        return (float)experience / GetExperienceToNextLevel(level);
    }
}
