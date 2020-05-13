﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To set up a quest.
/// </summary>

[System.Serializable]
public class Quest
{
    [SerializeField] string title;          // the title of the quest

    [TextArea(4, 10)]
    [SerializeField] string description;    // the description of the quest

    /// <summary>
    /// Returns the title of the quest.
    /// </summary>

    public string GetTitle
    {
        get { return title; }
    }

    /// <summary>
    /// Returns the description of the quest.
    /// </summary>

    public string GetDescription
    {
        get { return description; }
    }
}
