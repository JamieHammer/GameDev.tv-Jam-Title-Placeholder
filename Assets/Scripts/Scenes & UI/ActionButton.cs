using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the logic of the action buttons.
/// </summary>

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public ActionButtonType type;           // to set a button type

    InventorySlot slot;                     // reference to the inventory slot component of this button

    void Start()
    {
        slot = GetComponent<InventorySlot>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Responsible for updating the visual state of the action button icon.
    /// </summary>

    public void UpdateVisual()
    {
        slot.icon.sprite = MoveUI.instance.Put().icon;
        slot.icon.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (MoveUI.instance.moveable != null)
            {
                switch (type)
                {
                    case ActionButtonType.WeaponSlot:
                        if (true)   // check if type weapon
                        {
                            UpdateVisual();
                        }
                        break;

                    case ActionButtonType.QuickUseSlot:
                        if (true)   // check if type usable
                        {
                            UpdateVisual();
                        }
                        break;
                }
            }
        }
    }
}
