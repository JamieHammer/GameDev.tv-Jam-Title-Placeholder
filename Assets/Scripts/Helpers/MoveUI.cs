using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Makes it possible to move around UI elements, like dragging inventory items.
/// </summary>

public class MoveUI : MonoBehaviour
{
    #region Singleton

    public static MoveUI instance;

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

    public Moveable moveable;           // reference to the movable interface on an object

    Image icon;                         // reference to the image component of the object to move

    [SerializeField] Vector3 offset;    // an offset for the icon, so it isn't directly on the mouse position

    private void Start()
    {
        icon = GetComponent<Image>();
    }

    private void Update()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()
            && moveable != null)
        {
            DeleteItem();
        }
    }

    /// <summary>
    /// Assigns the movable to move.
    /// </summary>

    public void TakeMoveable(Moveable newMoveable)
    {
        moveable = newMoveable;
        icon.sprite = moveable.icon;
        icon.color = Color.white;
    }

    /// <summary>
    /// Used to put the held item in a designated place.
    /// </summary>
    /// <returns>the held item</returns>

    public Moveable Put()
    {
        Moveable tmp = moveable;

        moveable = null;

        icon.color = new Color(0, 0, 0, 0);

        return tmp;
    }

    /// <summary>
    /// Used to drop a held item.
    /// </summary>

    public void Drop()
    {
        moveable = null;

        icon.color = new Color(0, 0, 0, 0);

        InventoryManager.instance.movingSlot = null;
    }

    /// <summary>
    /// Used to delete an item from the inventory.
    /// </summary>

    public void DeleteItem()
    {
        if (moveable is Item)
        {
            Item item = (Item)moveable;

            if (item.Slot != null)
            {
                item.Slot.Clear();
            }

            if (item.equipmentSlot != null)
            {
                item.equipmentSlot.DequipItem();
            }
        }

        Drop();
        InventoryManager.instance.movingSlot = null;
    }
}
