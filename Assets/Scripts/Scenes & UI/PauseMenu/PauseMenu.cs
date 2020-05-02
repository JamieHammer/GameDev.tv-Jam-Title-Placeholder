using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Singleton
    public static PauseMenu instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    public static bool GameIsPaused = false;    // to check whether game is paused or not

    public GameObject pauseMenu;                // reference to the pause menu game object
    public GameObject settingsMenu;             // reference to the settings menu game object

    public Sprite mouseOver;                    // reference to the button mouse over sprite
    public Sprite mouseDown;                    // reference to the button mouse click sprite

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Responsible for resuming the game.
    /// </summary>

    public void Resume()
    {
        GameIsPaused = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    /// <summary>
    /// Responsible for pausing the game.
    /// </summary>

    void Pause()
    {
        GameIsPaused = true;

        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    /// <summary>
    /// Handles button clicks on the settings button.
    /// </summary>

    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Handles button clicks on the back button from the settings menu.
    /// </summary>

    public void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Handles button clicks on the load game button.
    /// </summary>

    public void LoadGame()
    {
        // todo remember to reset timescale to 1 when loading game

        Debug.Log("Load Game");
    }

    /// <summary>
    /// Handles button clicks on the quit game button.
    /// </summary>

    public void QuitGame()
    {
        // todo remove comment when implemented
        //Time.timeScale = 1f;

        Debug.Log("Quit Game");
    }
}
