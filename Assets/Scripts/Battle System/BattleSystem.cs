using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    #region Singleton
    public static BattleSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public BattleState battleState;

    public PlayerStats playerStats;

    public GameObject options;
    public GameObject endScreen;

    public GameObject[] playerPrefabs;
    public GameObject[] enemyPrefabs;

    public List<HudCtrl> playerHUDs = new List<HudCtrl>();
    public List<HudCtrl> enemyHUDs = new List<HudCtrl>();

    public Transform[] playerSpawnPoints;
    public Transform[] enemySpawnPoints;

    public PickTarget pickTarget;
    public bool[] stillAlive = new bool[4];

    [Header("UI")]

    public TextMeshProUGUI dialogueText;

    State state;

    List<PlayerStats> players = new List<PlayerStats>();
    public List<PlayerStats> enemies = new List<PlayerStats>();

    StoryManager storyManager;

    void Start()
    {
        SetupReferences();

        battleState = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    /// <summary>
    /// Sets up the references.
    /// </summary>

    void SetupReferences()
    {
        storyManager = StoryManager.instance;

        state = storyManager.GetCurrentStoryState();
        playerStats = storyManager.GetPlayer();
        enemyPrefabs = state.GetEnemyPrefabs();


    }

    bool allEnemiesAreDead()
    {
        for (int i = 0; i < stillAlive.Length; i++)
        {
            if (stillAlive[i])
            {
                return false;
            }
        }

        return true;
    }

    public static int count(bool[] array, bool flag)
    {
        int value = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == flag) value++;
        }

        return value;
    }

    /// <summary>
    /// Sets up the battle.
    /// </summary>

    IEnumerator SetupBattle()
    {
        // Player/ companion spawn

        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject playerGO = Instantiate(playerPrefabs[i], playerSpawnPoints[i]);
            players.Add(playerGO.GetComponent<PlayerStats>());

            playerSpawnPoints[i].gameObject.SetActive(true);

            playerHUDs[i].SetHUD(playerStats);
            playerHUDs[i].gameObject.SetActive(true);
        }

        // Enemy spawns

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject enemyGO = Instantiate(enemyPrefabs[i], enemySpawnPoints[i]);
            enemies.Add(enemyGO.GetComponent<PlayerStats>());

            enemySpawnPoints[i].gameObject.SetActive(true);

            PlayerStats enemyStats = enemyGO.GetComponent<PlayerStats>();

            enemyHUDs[i].SetHUD(enemyStats);
            enemyHUDs[i].gameObject.SetActive(true);

            stillAlive[i] = true;
        }

        dialogueText.text = "You have to fight your way out of this...";

        yield return new WaitForSeconds(2f);

        battleState = BattleState.PlayerTurn;
        PlayerTurn();
    }

    public IEnumerator PlayerAttack(PlayerStats target)
    {
        int index = 0;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == target)
            {
                index = i;
            }
        }

        players[0].GetComponentInChildren<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        target.GetComponentInChildren<Animator>().SetTrigger("Hurt");

        yield return new WaitForSeconds(1f);

        bool isDead = target.TakeDamage(playerStats.attack.GetValue());

        enemyHUDs[index].SetHP(target.currentHealth);
        dialogueText.text = "The attack was succesful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            target.GetComponentInChildren<Animator>().SetTrigger("Dead");

            stillAlive[index] = false;

            DeactivateHUD(index);
        }

        if (allEnemiesAreDead())
        {
            battleState = BattleState.Won;
            EndBattle();
        }
        else
        {
            battleState = BattleState.EnemyTurn;

            foreach (PlayerStats enemy in enemies)
            {
                StartCoroutine(EnemyTurn(enemy));
            }
        }
    }

    IEnumerator PlayerItem()
    {
        playerStats.Heal(5);
        playerHUDs[0].SetHP(playerStats.currentHealth);

        dialogueText.text = "You feel fully replenished...";

        yield return new WaitForSeconds(2f);

        battleState = BattleState.EnemyTurn;

        foreach (PlayerStats enemy in enemies)
        {
            StartCoroutine(EnemyTurn(enemy));
        }
    }

    IEnumerator EnemyTurn(PlayerStats enemy)
    {
        dialogueText.text = enemy.characterName + " attacks!";

        enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        players[0].GetComponentInChildren<Animator>().SetTrigger("Hurt");

        bool isDead = playerStats.TakeDamage(enemy.attack.GetValue());

        playerHUDs[0].SetHP(playerStats.currentHealth);

        yield return new WaitForSeconds(1);

        if (isDead)
        {
            players[0].GetComponentInChildren<Animator>().SetTrigger("Dead");

            battleState = BattleState.Lost;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PlayerTurn;

            PlayerTurn();
        }
    }

    void EndBattle()
    {
        var currentState = storyManager.GetCurrentStoryState();

        State[] nextStates = currentState.GetNextStates();

        if (battleState == BattleState.Won)
        {
            dialogueText.text = "You won!";
            state = nextStates[0];
        }
        else
        {
            dialogueText.text = "You lost...";
            state = nextStates[1];
        }

        endScreen.SetActive(true);
    }

    void PlayerTurn()
    {
        dialogueText.text = "Please choose an action...";
        options.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        options.SetActive(false);

        int pos = count(stillAlive, true);

        if (pos > 1)
        {
            dialogueText.text = "Please choose a target";

            pickTarget.ShowTargets();
        }
        else
        {
            for (int i = 0; i < stillAlive.Length; i++)
            {
                if (stillAlive[i])
                {
                    StartCoroutine(PlayerAttack(enemies[0]));
                }
            }
        }
    }

    public void OnItemButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerItem());

        options.SetActive(false);
    }

    public void onContinueButton()
    {
        storyManager.AdvanceStory(state);

        Destroy(this.gameObject);
    }

    void DeactivateHUD(int index)
    {
        stillAlive[index] = false;

        enemyHUDs[index].gameObject.SetActive(false);
    }
}
