using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudCtrl : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvlText;
    public Image sliderFill;

    public Color32 colourGreen;
    public Color32 colourYellow;
    public Color32 colourRed;

    public void SetHUD(PlayerStats stats)
    {
        nameText.text = stats.characterName;
        healthBar.maxValue = stats.maxHealth;
        healthBar.value = stats.currentHealth;

        if (healthBar.value >= 0.5f)
        {
            ChangeSliderColour(colourGreen);
        }
        else if (healthBar.value < 0.5f && healthBar.value >= 0.2f)
        {
            ChangeSliderColour(colourYellow);
        }
        else if (healthBar.value < 0.2f)
        {
            ChangeSliderColour(colourRed);
        }
    }

    void ChangeSliderColour(Color32 colour)
    {
        sliderFill.color = colour;
    }

    public void SetHP(int hp)
    {
        healthBar.value = hp;

        if (healthBar.value >= 0.5f)
        {
            ChangeSliderColour(colourGreen);
        }
        else if (healthBar.value < 0.5f && healthBar.value >= 0.2f)
        {
            ChangeSliderColour(colourYellow);
        }
        else if (healthBar.value < 0.2f)
        {
            ChangeSliderColour(colourRed);
        }
    }
}
