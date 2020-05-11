using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

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

    [SerializeField] TextMeshProUGUI toolInfo;          // the text component of the effect information

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
        toolInfo.text = toolTipInfo();
    }

    string toolTipInfo()
    {
        StringBuilder info = new StringBuilder();

        switch (item.GetInventoryType())
        {
            case InventoryType.None:
                break;

            case InventoryType.Equipment:
                Debug.Log((item as Equipment).GetAttackModifier());
                break;

            case InventoryType.Usable:

                break;

            case InventoryType.Quest:

                break;
        }

        return "yo, info... and shit.";
    }

    #endregion
}
