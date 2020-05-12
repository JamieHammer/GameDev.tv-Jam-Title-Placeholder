using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages global UI.
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

    #region Tooltip

    [SerializeField] GameObject tooltip;                // reference to the tooltip object

    [Header("Text references")]

    [SerializeField] TextMeshProUGUI toolTitle;         // the text component of the title text

    [SerializeField] TextMeshProUGUI toolDescription;   // the text component of the description

    [Space]

    List<int> modifiers = new List<int>();              // a list of modifiers

    List<GameObject> infoRows = new List<GameObject>(); // a list of effect info game objects

    int modifierCount;                                  // the count of modifiers

    Item item;                                          // reference to the currently highlighted item

    /// <summary>
    /// Responsible for showing the tooltip.
    /// </summary>
    /// <param name="position">The position of the highlighted slot</param>
    /// <param name="newItem">the item, which info is shown</param>

    public void ShowTooltip(Vector3 position, Item newItem)
    {
        item = newItem;

        RefreshTooltip();

        tooltip.SetActive(true);
        tooltip.transform.position = position;
    }

    /// <summary>
    /// Responsible for hiding the tooltip.
    /// </summary>

    public void HideTooltip()
    {
        tooltip.SetActive(false);

        item = null;
    }

    /// <summary>
    /// Responsible for refreshing the tooltip.
    /// </summary>

    public void RefreshTooltip()
    {
        toolTitle.text = item.GetName();
        toolDescription.text = item.GetDescription();

        EffectInfo();
    }

    void EffectInfo()
    {
        modifiers.Clear();
        modifierCount = 0;

        modifiers = item.GetModifiers();

        int count = modifiers.Count;     // eight abilities and two status effects

        for (int i = 0; i < count; i++)
        {
            if (modifiers[i] != 0)
            {
                ShowEffectInfo(i, modifiers[i]);
            }
        }
    }

    void ShowEffectInfo(int index, int value)
    {
        Debug.Log("the index " + index + " is " + value);

        switch (modifierCount)
        {
            default:
                break;
        }

        modifierCount++;

        Debug.Log(modifierCount);
    }

    #endregion
}
