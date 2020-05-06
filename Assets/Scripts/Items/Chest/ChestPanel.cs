using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class ChestPanel : MonoBehaviour
{
    #region Singleton
    public static ChestPanel instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] GameObject chestSlotParent;        // reference to the chest slots parent

    List<Item> chestSlots;                              // a list of items

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
