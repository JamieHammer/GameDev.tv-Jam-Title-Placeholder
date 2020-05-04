using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// </summary>

public class CharacterUI : MonoBehaviour
{
    #region Singleton

    public static CharacterUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("something is wrong here");
        }
    }

    #endregion

    [SerializeField] Player  currentStats;       // the character stats of the player

    [Header("")]

    [SerializeField] TextMeshProUGUI charName;      // reference to the character name text component

    [SerializeField] GameObject player;             // reference to the player character

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Responsible for updating the UI of the character info.
    /// </summary>

    void UpdateUI()
    {
        charName.text = currentStats.characterName;
    }

    /// <summary>
    /// Returns the current stats for the abilities UI class to use.
    /// </summary>

    public Player  GetCurrentStats()
    {
        return currentStats;
    }
}
