using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolsUI : MonoBehaviour
{
    #region Singleton

    public static ToolsUI instance;         // singleton

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetupTools();
    }

    #endregion

    public TextMeshProUGUI titleText;           // the title text component

    [SerializeField] GameObject character;      // the character tool
    [SerializeField] GameObject quests;         // the quests tool
    [SerializeField] GameObject map;            // the map tool

    ToolsManager tools;                         // reference to the tools manager

    private void OnEnable()
    {
        UpdateUI();
    }

    /// <summary>
    /// Responsible for setting up the slots before deactivating itself.
    /// </summary>

    private void SetupTools()
    {
        tools = ToolsManager.instance;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Iterates through all inventory items and updates the UI.
    /// </summary>

    public void UpdateUI()
    {
        if (tools == null)
        {
            tools = ToolsManager.instance;
        }

        HideAll();

        switch (tools.currentTool)
        {
            case Tool.None:

                break;

            case Tool.Character:
                UpdateTitle("Character");
                character.SetActive(true);
                break;

            case Tool.Quests:
                UpdateTitle("Quests");
                quests.SetActive(true);
                break;

            case Tool.Map:
                UpdateTitle("Map");
                map.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Hides all of the inventory slots.
    /// </summary>

    void HideAll()
    {
        character.SetActive(false);
        quests.SetActive(false);
        map.SetActive(false);
    }

    /// <summary>
    /// Updates the title of the inventory window.
    /// </summary>
    /// <param name="title">the active title</param>

    void UpdateTitle(string title)
    {
        titleText.text = title;
    }
}
