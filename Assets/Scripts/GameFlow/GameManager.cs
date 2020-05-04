using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Debugging

    public bool isNewGame;

    #endregion

    Player player;                  // reference to the player

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        if (isNewGame)
        {
            NewGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        player.NewPlayer();
    }
}
