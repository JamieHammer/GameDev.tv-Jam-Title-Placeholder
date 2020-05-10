using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        tools = ToolsManager.instance;

        SetupTools();
    }

    #endregion

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
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Iterates through all inventory items and updates the UI.
    /// </summary>

    public void UpdateUI()
    {
        HideAll();

        switch (tools.currentTool)
        {
            case Tool.None:

                break;

            case Tool.Character:
                character.SetActive(true);
                break;

            case Tool.Quests:
                quests.SetActive(true);
                break;

            case Tool.Map:
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
}
