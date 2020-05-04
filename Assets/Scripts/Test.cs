using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayerWindow playerWindow;
    [SerializeField] private PlayerStats player;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        playerWindow.SetLevelSystem(levelSystem);
        player.SetLevelSystem(levelSystem);
    }
}
