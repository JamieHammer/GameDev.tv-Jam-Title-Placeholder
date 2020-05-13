using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    #region Singleton
    public static ChestManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] Bag bag;                   // a reference to the bag component

    List<Item> items;                           // a list of items contained in this chest

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

        bag.Clear();
    }
}
