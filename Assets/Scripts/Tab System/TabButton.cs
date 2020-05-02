using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Responsible for tab buttons and their content.
/// </summary>

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] TabGroup tabGroup;             // reference to the tab group
    [SerializeField] Image background;              // reference to the background of this button
    [SerializeField] TextMeshProUGUI titleTxt;      // reference to the title text element

    public UnityEvent onTabSelected;                // to invoke the selected callback on this tab
    public UnityEvent onTabDeselected;              // to invoke the deselected callback on this tab

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Awake()
    {
        background = GetComponent<Image>();
        titleTxt = GetComponentInChildren<TextMeshProUGUI>();        
    }

    /// <summary>
    /// Responsible for updating the background colour of this tab button.
    /// </summary>
    /// <param name="colour">The preferred colour</param>

    public void UpdateBackground(Color32 colour)
    {
        background.color = colour;
    }

    /// <summary>
    /// Responsible for updating the text colour of this tab button.
    /// </summary>
    /// <param name="colour"></param>

    public void UpdateText(Color32 colour)
    {
        titleTxt.color = colour;
    }

    /// <summary>
    /// Invokes the callback upon tab selection.
    /// </summary>

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    /// <summary>
    /// Invokes the callback upon tab unselection.
    /// </summary>

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    /// <summary>
    /// Handles button click on the exit game button.
    /// </summary>

    public void QuitGame()
    {
        Debug.Log("Leaving the building like Elvis!");

        Application.Quit();
    }
}
