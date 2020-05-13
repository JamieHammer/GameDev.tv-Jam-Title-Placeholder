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

    private void Start()
    {
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
            ChestManager.instance.AddItems();

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
            ChestManager.instance.StoreItems();

            isOpen = false;

            animator.SetBool("Open", false);

            chestPanel.gameObject.SetActive(false);
        }
    }
}
