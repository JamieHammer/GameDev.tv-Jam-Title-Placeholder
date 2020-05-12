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

    [SerializeField] List<EffectInfo> infoRows = new List<EffectInfo>(); // a list of effect info components

    [SerializeField] Image icon;                        // the image component of the icon

    List<int> modifiers = new List<int>();              // a list of modifiers

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
        icon.sprite = item.icon;
        toolTitle.text = item.GetName();
        toolDescription.text = item.GetDescription();

        EffectInfo();
    }

    /// <summary>
    /// Handles the effect information.
    /// </summary>

    void EffectInfo()
    {
        modifiers.Clear();
        modifierCount = 0;
        HideAllInfoRows();

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

    /// <summary>
    /// Responsible for reseting the effect info rows.
    /// </summary>

    void HideAllInfoRows()
    {
        for (int i = 0; i < infoRows.Count; i++)
        {
            infoRows[i].HideInfo();
            infoRows[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Responsible for showing the correct row and query the string.
    /// </summary>
    /// <param name="index">this is the index of the stat</param>
    /// <param name="value">this is the value of the modifier</param>

    void ShowEffectInfo(int index, int value)
    {
        switch (modifierCount)
        {
            case 0:
                infoRows[0].gameObject.SetActive(true);
                infoRows[0].ShowFirstInfo(InfoText(index, value));
                break;

            case 1:
                infoRows[0].ShowSecondInfo(InfoText(index, value));
                break;

            case 2:
                infoRows[1].gameObject.SetActive(true);
                infoRows[1].ShowFirstInfo(InfoText(index, value));
                break;

            case 3:
                infoRows[1].ShowSecondInfo(InfoText(index, value));
                break;

            case 4:
                infoRows[2].gameObject.SetActive(true);
                infoRows[2].ShowFirstInfo(InfoText(index, value));
                break;

            case 5:
                infoRows[2].ShowSecondInfo(InfoText(index, value));
                break;

            case 6:
                infoRows[3].gameObject.SetActive(true);
                infoRows[3].ShowFirstInfo(InfoText(index, value));
                break;

            case 7:
                infoRows[3].ShowSecondInfo(InfoText(index, value));
                break;
        }

        modifierCount++;
    }

    /// <summary>
    /// This returns a string to send to the effect info class.
    /// </summary>

    string InfoText(int index, int value)
    {
        StringBuilder info = new StringBuilder();

        switch (index)
        {
            case 0:     // Strength
                info.Append("Strength: ");
                break;

            case 1:     // Stamina
                info.Append("Stamina: ");
                break;

            case 2:     // Intelligence
                info.Append("Intelligence: ");
                break;

            case 3:     // Dexterity
                info.Append("Dexterity: ");
                break;

            case 4:     // Charisma
                info.Append("Charisma: ");
                break;

            case 5:     // Luck
                info.Append("Luck: ");
                break;

            case 6:     // Attack
                info.Append("Attack: ");
                break;

            case 7:     // Defence
                info.Append("Defence: ");
                break;

            case 8:     // Health
                info.Append("Health: ");
                break;

            case 9:     // Experience
                info.Append("Experience: ");
                break;
        }

        if (value > 0)
        {
            info.Append("+" + value);
        }
        else
        {
            info.Append(value);
        }

        string infoText = info.ToString();

        return infoText;
    }

    #endregion
}
