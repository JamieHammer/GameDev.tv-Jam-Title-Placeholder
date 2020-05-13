using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject questPrefab;

    [SerializeField] Transform questParent;

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);

        go.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetTitle;
    }
}
