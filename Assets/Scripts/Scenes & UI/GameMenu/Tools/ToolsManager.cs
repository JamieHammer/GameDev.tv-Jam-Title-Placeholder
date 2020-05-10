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
    }

    #endregion

    public TextMeshProUGUI titleText;                   // the text component of the title

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
}
