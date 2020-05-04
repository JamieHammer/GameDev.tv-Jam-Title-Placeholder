using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Responsible for UI related to the player's level.
/// </summary>

public class PlayerWindow : MonoBehaviour
{
    #region Singleton
    public static PlayerWindow instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    [SerializeField] TextMeshProUGUI nameText;                  // reference to the name text component
    [SerializeField] TextMeshProUGUI levelText;                 // reference to the level text component
    [SerializeField] TextMeshProUGUI healthText;                // reference to the health text component

    [SerializeField] Slider healthBar;                          // reference to the health slider component
    [SerializeField] Slider experienceBar;                      // reference to the experience slider component

    [SerializeField] Image healthFill;                          // reference to the image component of the health bar's fill area

    Color32 colourGreen = new Color32(125, 216, 87, 255);       // reference to the green health bar colour
    Color32 colourYellow = new Color32(255, 189, 74, 255);      // reference to the yellow health bar colour
    Color32 colourRed = new Color32(198, 68, 39, 255);          // reference to the red health bar colour

    [SerializeField] PlayerStats player;                        // reference to the current player stats

    private LevelSystemAnimation levelSystemAnimation;          // reference to the level system animator

    /// <summary>
    /// Responsible for setting up the reference to the current player and update UI.
    /// </summary>
    /// <param name="playerStats">the stats of the player</param>

    public void SetPlayer(PlayerStats playerStats)
    {
        player = playerStats;

        UpdateName();

        UpdateHealth();

        UpdateExperience();
    }

    /// <summary>
    /// Responsible for updating the player health UI.
    /// </summary>

    public void SetHP()
    {
        UpdateHealth();
    }

    /// <summary>
    /// Responsible for updating the player experience UI.
    /// </summary>

    public void SetExperience()
    {
        UpdateExperience();
    }

    #region Name

    /// <summary>
    /// Responsible for updating the player name.
    /// </summary>

    void UpdateName()
    {
        nameText.text = player.characterName;
    }

    #endregion

    #region Health

    /// <summary>
    /// Responsible for updating the player health.
    /// </summary>

    void UpdateHealth()
    {
        SetHealthBarSize(player.GetHealthNormalised());
        SetHealthStatus(player.currentHealth);
    }

    /// <summary>
    /// Responsible for showing the remaining health in relation to max health.
    /// </summary>
    /// <param name="healthNormalised">the remaining health</param>

    void SetHealthBarSize(float healthNormalised)
    {
        healthBar.value = healthNormalised;

        if (healthBar.value >= 0.5f)
        {
            UpdateHealthBarColour(colourGreen);
        }
        else if (healthBar.value < 0.5f && healthBar.value >= 0.2f)
        {
            UpdateHealthBarColour(colourYellow);
        }
        else if (healthBar.value < 0.2f)
        {
            UpdateHealthBarColour(colourRed);
        }
    }

    /// <summary>
    /// Responsible for updating the colour of the health bar.
    /// </summary>
    /// <param name="colour">the preferred colour</param>

    void UpdateHealthBarColour(Color32 colour)
    {
        healthFill.color = colour;
    }

    /// <summary>
    /// Responsible for showing the remaining health.
    /// </summary>

    void SetHealthStatus(int currentHealth)
    {
        healthText.text = currentHealth + "/" + player.maxHealth;
    }

    #endregion

    #region Experience

    /// <summary>
    /// Responsible for updating the player experience.
    /// </summary>

    void UpdateExperience()
    {
        SetExperienceBarSize(levelSystemAnimation.GetExperienceNormalised());
        SetLevelNumber(levelSystemAnimation.GetLevelNumber());
    }

    /// <summary>
    /// Responsible for showing the progress in relation to the next level.
    /// </summary>
    /// <param name="experienceNormalised">the progress to next level</param>

    void SetExperienceBarSize(float experienceNormalised)
    {
        experienceBar.value = experienceNormalised;
    }

    /// <summary>
    /// Responsible for showing the player level.
    /// </summary>
    /// <param name="levelNumber">the levelnumber to show</param>

    void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Lvl " + (levelNumber + 1);
    }

    /// <summary>
    /// Responsible for setting a reference to a level system animator.
    /// </summary>
    /// <param name="levelSystemAnimation">the level system animator to animate the experience bar</param>

    public void SetLevelSystemAnimation(LevelSystemAnimation levelSystemAnimation)
    {
        // set the level system animation object
        this.levelSystemAnimation = levelSystemAnimation;

        // update the starting values
        UpdateExperience();

        // subscrie to the changed events
        levelSystemAnimation.OnExperienceChanged += LevelSystemAnimation_OnExperienceChanged;
        levelSystemAnimation.OnLevelChanged += LevelSystemAnimation_OnLevelChanged;
    }

    /// <summary>
    /// Subscribe to the level system's on level changed, callback.
    /// </summary>

    private void LevelSystemAnimation_OnLevelChanged(object sender, System.EventArgs e)
    {
        // level changed, update text
        SetLevelNumber(levelSystemAnimation.GetLevelNumber());
    }

    /// <summary>
    /// Subscribe to the level system's on experience changed, callback.
    /// </summary>

    private void LevelSystemAnimation_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // experience changed, update bar size
        SetExperienceBarSize(levelSystemAnimation.GetExperienceNormalised());
    }

    #endregion
}
