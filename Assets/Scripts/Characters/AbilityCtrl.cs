using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Controls individual abilities.
/// </summary>

public class AbilityCtrl : MonoBehaviour
{
    Stat stat;                                          // the current stat

    [SerializeField] TextMeshProUGUI titleTxt;          // the text component of the ability title
    [SerializeField] TextMeshProUGUI valueTxt;          // the text component of the base value
    [SerializeField] TextMeshProUGUI modifierTxt;       // the text component of potential modifiers

    /// <summary>
    /// Sets the current stat values from the Ability UI class.
    /// </summary>

    public void SetActiveStat(Stat newStat)
    {
        stat = newStat;

        titleTxt.text = stat.GetName();
        valueTxt.text = stat.GetBaseValue().ToString();

        int modifier = stat.GetModifierValue();

        if (modifier == 0)
        {
            modifierTxt.text = "";
        }
        else if (modifier < 0)
        {
            modifierTxt.text = modifier.ToString();
            NegativeModifier();
        }
        else if (modifier > 0)
        {
            modifierTxt.text = "+" + modifier;
            PositiveModifier();
        }
    }

    /// <summary>
    /// Sets the modifier text colour to green.
    /// </summary>

    void PositiveModifier()
    {
        modifierTxt.color = new Color32(125, 216, 87, 255);
    }

    /// <summary>
    /// Sets the modifier text colour to red.
    /// </summary>

    void NegativeModifier()
    {
        modifierTxt.color = new Color32(198, 68, 39, 255);
    }
}
