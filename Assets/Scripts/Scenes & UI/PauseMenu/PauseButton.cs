using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the button transitions for the pause menu buttons.
/// </summary>

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    PauseMenu pauseMenu;                // reference to the pause menu

    Image button;                       // reference to the image component of this button
    TextMeshProUGUI buttonText;         // reference to the text component of this button

    void Start()
    {
        pauseMenu = PauseMenu.instance;
        button = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        ResetButton();
    }

    /// <summary>
    /// Sets the button to show and it's text to white, on mouse over.
    /// </summary>
    /// <param name="eventData"></param>

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = new Color32(255, 255, 255, 255);
        button.color = new Color32(255, 255, 255, 255);
    }

    /// <summary>
    /// Responsible for reseting the button on exit events.
    /// </summary>

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetButton();
    }

    /// <summary>
    /// Responsible for reseting the button on click events.
    /// </summary>

    public void OnPointerClick(PointerEventData eventData)
    {
        ResetButton();
    }

    /// <summary>
    /// Resets the button to transparent with dark text and unclicked button sprite.
    /// </summary>

    void ResetButton()
    {
        buttonText.color = new Color32(34, 34, 34, 255);
        button.color = new Color32(255, 255, 255, 0);
        button.sprite = pauseMenu.mouseOver;
    }
}
