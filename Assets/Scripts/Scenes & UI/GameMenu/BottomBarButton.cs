using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the menu select buttons in the bottom bar.
/// </summary>

public class BottomBarButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string newTitle;             // the temporary title to show on hover

    public bool isInventory;            // whether the buttons belong to the inventory or tools menu

    /// <summary>
    /// Sets the title text to the hovered button, temporarily.
    /// </summary>

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInventory)
        {
            InventoryManager.instance.UpdateTitle(newTitle);
        }
        else
        {
            ToolsManager.instance.UpdateTitle(newTitle);
        }
    }

    /// <summary>
    /// Sets the title text back to it's original state.
    /// </summary>

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInventory)
        {
            InventoryManager.instance.RevertTitle();
        }
        else
        {
            ToolsManager.instance.RevertTitle();
        }
    }
}
