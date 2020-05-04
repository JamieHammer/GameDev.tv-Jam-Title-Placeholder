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

        LevelSystemAnimation levelSystemAnimation = new LevelSystemAnimation(levelSystem);
        playerWindow.SetLevelSystemAnimation(levelSystemAnimation);
        player.SetLevelSystemAnimation(levelSystemAnimation);

        string time = "" + System.DateTime.Now;

        Debug.Log(time);
    }
}
