using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages UI globaly.
/// </summary>

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

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

    /// <summary>
    /// Responsible for hiding the tooltip.
    /// </summary>

    public void HideToolTip()
    {
        // todo
    }

    /// <summary>
    /// Responsible for refreshing the tooltip.
    /// </summary>

    public void RefreshToolTip()
    {
        // tooltipText.text = description.GetDescription();
    }
}
