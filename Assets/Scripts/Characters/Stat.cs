using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private string statName;

    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    /// <summary>
    /// Returns the stat value.
    /// </summary>

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    /// <summary>
    /// Returns the base stat value.
    /// </summary>

    public int GetBaseValue()
    {
        return baseValue;
    }

    /// <summary>
    /// Returns the accumulated modifier value.
    /// </summary>

    public int GetModifierValue()
    {
        int modifierValue = 0;

        if (modifiers.Count > 0)
        {
            modifiers.ForEach(x => modifierValue += x);
        }

        return modifierValue;
    }

    /// <summary>
    /// Responsible for adding this modifier to the character stats.
    /// </summary>
    /// <param name="modifier">the modifier amount to add</param>

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    /// <summary>
    /// Responsible for removing this modifier from the character stats.
    /// </summary>
    /// <param name="modifier">the modifier to remove</param>

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }

    /// <summary>
    /// Returns the name of this stat.
    /// </summary>

    public string GetName()
    {
        return statName;
    }

    public void SetName(string name)
    {
        statName = name;
    }
}
