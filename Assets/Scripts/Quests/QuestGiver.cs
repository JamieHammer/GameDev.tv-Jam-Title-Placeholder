﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest[] quests;         // an array of quests held by this quest giver

    // ONLY FOR DEBUGGING - REMOVE AFTER
    public QuestLog tmpLog;

    private void Awake()
    {
        tmpLog.AcceptQuest(quests[0]);
    }
}