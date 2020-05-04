using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    public Transform abilitiesParent;           // the parent transform of the abilities

    InventoryManager inventory;                 // reference to the inventory manager
    EquipmentManager equipment;                 // reference to the equipment manager

    AbilityCtrl[] currentAbilities;             // an array of ability controllers

    Player  currentStats;                       // reference to the current stats

    // Start is called before the first frame update
    void Start()
    {
        currentAbilities = abilitiesParent.GetComponentsInChildren<AbilityCtrl>();

        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        equipment = EquipmentManager.instance;
        equipment.onEquipmentChanged += UpdateHelper;

        UpdateUI();
    }

    /// <summary>
    /// Calls the update method on ability value change.
    /// </summary>

    void UpdateHelper(Equipment newItem, Equipment oldItem)
    {
        UpdateUI();
    }

    /// <summary>
    /// Iterates through all ability controllers and updates the UI.
    /// </summary>

    void UpdateUI()
    {
        currentStats = CharacterUI.instance.GetCurrentStats();

        for (int i = 0; i < currentAbilities.Length; i++)
        {
            switch (i)
            {
                case 0:
                    currentAbilities[i].SetActiveStat(currentStats.strength);
                    break;

                case 1:
                    currentAbilities[i].SetActiveStat(currentStats.stamina);
                    break;

                case 2:
                    currentAbilities[i].SetActiveStat(currentStats.intelligence);
                    break;

                case 3:
                    currentAbilities[i].SetActiveStat(currentStats.dexterity);
                    break;

                case 4:
                    currentAbilities[i].SetActiveStat(currentStats.charisma);
                    break;

                case 5:
                    currentAbilities[i].SetActiveStat(currentStats.luck);
                    break;

                case 6:
                    currentAbilities[i].SetActiveStat(currentStats.attack);
                    break;

                case 7:
                    currentAbilities[i].SetActiveStat(currentStats.defence);
                    break;
            }
        }
    }
}
