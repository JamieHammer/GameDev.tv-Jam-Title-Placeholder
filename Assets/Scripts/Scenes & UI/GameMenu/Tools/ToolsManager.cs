using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the tools bar and it's tool windows.
/// </summary>

public class ToolsManager : MonoBehaviour
{
    #region Singleton

    public static ToolsManager instance;

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

        toolsUI = ToolsUI.instance;
    }

    #endregion

    public TextMeshProUGUI titleText;                   // the text component of the title

    public GameObject toolsWindow;                      // reference to the tools window
    ToolsUI toolsUI;                                    // reference to the tools UI

    public Tool currentTool;                            // the current tool selected

    /// <summary>
    /// To show the title of the hovered inventory bag, while hovered.
    /// </summary>
    /// <param name="newTitle">The new title to show</param>

    public void UpdateTitle(string newTitle)
    {
        titleText.text = newTitle;
    }

    /// <summary>
    /// To revert back to the title after hover has ended.
    /// </summary>

    public void RevertTitle()
    {
        titleText.text = "Tools";
    }

    /// <summary>
    /// Responsible for showing character tool.
    /// </summary>

    public void ToggleCharacter()
    {
        if (currentTool != Tool.Character)
        {
            currentTool = Tool.Character;
            ShowTool();
        }
        else
        {
            HideTool();
        }
    }

    /// <summary>
    /// Responsible for showing character tool.
    /// </summary>

    public void ToggleQuests()
    {
        if (currentTool != Tool.Quests)
        {
            currentTool = Tool.Quests;
            ShowTool();
        }
        else
        {
            HideTool();
        }
    }

    /// <summary>
    /// Responsible for showing character tool.
    /// </summary>

    public void ToggleMap()
    {
        if (currentTool != Tool.Map)
        {
            currentTool = Tool.Map;
            ShowTool();
        }
        else
        {
            HideTool();
        }
    }

    /// <summary>
    /// Responsible for showing the tools window.
    /// </summary>

    void ShowTool()
    {
        if (toolsUI == null)
        {
            toolsUI = ToolsUI.instance;
        }

        toolsWindow.SetActive(true);
        toolsUI.UpdateUI();
    }

    /// <summary>
    /// Responsible for hiding the tools window.
    /// </summary>

    public void HideTool()
    {
        toolsWindow.SetActive(false);
        currentTool = Tool.None;
    }
}
