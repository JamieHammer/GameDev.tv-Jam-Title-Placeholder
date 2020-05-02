using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickTarget : MonoBehaviour
{
    BattleSystem battleSystem;

    public GameObject[] buttons = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = BattleSystem.instance;

        HideTargets();
    }

    public void ChooseTarget(int index)
    {
        Debug.Log("Target " + index + " is chosen");

        HideTargets();

        StartCoroutine(battleSystem.PlayerAttack(battleSystem.enemies[index]));
    }

    public void ShowTargets()
    {
        TextMeshProUGUI buttonTitle;

        for (int i = 0; i < battleSystem.stillAlive.Length; i++)
        {
            if (battleSystem.stillAlive[i])
            {
                // button title
                buttonTitle = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonTitle.text = battleSystem.enemies[i].characterName;

                // show button
                buttons[i].SetActive(true);
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void HideTargets()
    {
        for (int i = 0; i < battleSystem.stillAlive.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }
}
