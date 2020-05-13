using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class Chest : MonoBehaviour, Interactable
{
    [SerializeField] ChestPanel chestPanel;     // reference to the chest panel

    SpriteRenderer spriteRenderer;              // reference to the sprite renderer component
    Animator animator;                          // reference to the animator component

    bool isOpen;                                // to check whether the chest is open or closed

    List<Item> items;                           // a list of items contained in this chest
    Bag bag;                                    // a reference to the bag component

    private void Start()
    {
        bag = chestPanel.GetComponent<Bag>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// To open or close a chest when the player is in range.
    /// </summary>

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();

            isOpen = true;

            animator.SetBool("Open", true);

            chestPanel.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Stops interaction with the chest.
    /// </summary>

    public void StopInteract()
    {
        if (isOpen)
        {
            StoreItems();

            bag.Clear();

            isOpen = false;

            animator.SetBool("Open", false);

            chestPanel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>

    public void AddItems()
    {
        if (items != null)
        {
            foreach (var item in items)
            {
                item.Slot.AddItem(item);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>

    public void StoreItems()
    {
        items = bag.GetItems();
    }
}
