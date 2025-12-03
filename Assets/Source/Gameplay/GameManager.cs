using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public class RunStats
    {
        public int ActionsTaken;
        public int DamageDealt;
        public int AttacksDodged;
        public int DamageBlocked;
        public int LevelsCleared;
    }

    public RunStats roundStats = new RunStats();
    public RunStats totalStats = new RunStats();

    // MENU NAVIGATION
    public GameObject StartScreen;
    public GameObject GameplayScreen;
    public GameObject LevelCompleteScreen;
    public GameObject GameOverScreen;
    //

    // UI ELEMENTS
    public GameObject LevelText;
    public GameObject TurnText;
    public GameObject InfoText;
    public GameObject AttackButton;
    public GameObject BlockButton;
    public GameObject ContinueButton;
    public GameObject EndTurnButton;
    public GameObject m_apTracker;
    //

    private LevelComplete levelComplete;

    private PlayerScript playerScript;
    private EnemyScript enemyScript;

    private int m_turn;
    public bool m_playerTurn;
    private float m_enemyActionTimer;

    [SerializeField]
    GameObject m_healButton;
    [SerializeField]
    GameObject m_healText;
    private bool m_healCharge;

    [SerializeField]
    private AudioManager m_audioManager;

    [SerializeField]
    private GameObject m_player;
    private Animator m_playerAnimator;

    [SerializeField]
    private GameObject m_enemy;
    private Animator m_enemyAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAll();
        StartScreen.SetActive(true);
        playerScript = GetComponentInParent<PlayerScript>();
        enemyScript = GetComponentInParent<EnemyScript>();
        levelComplete = LevelCompleteScreen.GetComponent<LevelComplete>();
        totalStats.LevelsCleared = 0;
        m_playerAnimator = m_player.GetComponent<Animator>();
        m_enemyAnimator = m_enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_playerTurn)
        {
            EnemyDecisionMaking();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel()
    {
        // THERE ARE ONLY 10 LEVELS IMPLEMENTED
        if (totalStats.LevelsCleared >= 10)
        {
            ReturnToStart();
            return;
        }

        // Reset round specific stats
        roundStats = new RunStats();

        // Disaplay and hide correct UI
        DisableAll();
        GameplayScreen.SetActive(true);
        AttackButton.SetActive(true);
        BlockButton.SetActive(true);
        ContinueButton.SetActive(false);
        EndTurnButton.SetActive(false);
        m_healButton.SetActive(true);
        m_healText.SetActive(true);
        m_apTracker.SetActive(true);

        // Initialise player and enemy
        playerScript.NewTurn();
        enemyScript.Init(totalStats.LevelsCleared);

        // Display relevant level start info
        SetInfoText("Your Turn");
        m_playerTurn = true;
        m_turn = 1;
        TurnText.GetComponent<TextMeshProUGUI>().text = "Turn " + m_turn;
        LevelText.GetComponent<TextMeshProUGUI>().text = "Level " + (totalStats.LevelsCleared + 1);

        m_healText.GetComponent<TextMeshProUGUI>().text = "1/1";
        m_healCharge = true;
}

    public void LevelWon()
    {
        levelComplete.roundWonValues();
        DisableAll();
        LevelCompleteScreen.SetActive(true);
    }

    public void LevelLost() 
    {
        playerScript.InitStats();
        levelComplete.gameOverValues();
        DisableAll();
        GameOverScreen.SetActive(true);
    }

    public void ReturnToStart()
    {
        DisableAll();
        StartScreen.SetActive(true);
        roundStats = new RunStats();
        totalStats = new RunStats();
        totalStats.LevelsCleared = 0;
    }

    private void DisableAll()
    {
        StartScreen.SetActive(false);
        GameplayScreen.SetActive(false);
        LevelCompleteScreen.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    public void PlayerAttack()
    {
        PlayerScript.GameplayStats playerStats = playerScript.GetStats();
        EnemyScript.EnemyStats enemyStats = enemyScript.GetStats();

        m_playerAnimator.SetTrigger("Attack");

        if (playerStats.m_hitChance > Random.Range(0.0f, 100.0f))
        {
            m_enemyAnimator.SetTrigger("Hit");
            m_audioManager.PlayHit();
            int damageToDeal = playerStats.m_attackDamage;
            bool crit = false;

            if (playerStats.m_critChance > Random.Range (0.0f, 100.0f))
            {
                crit = true;
                damageToDeal = playerStats.m_attackDamage * 2;
            }

            if (enemyStats.m_currentBlock > 0)
            {
                int newEnemyBlock = enemyStats.m_currentBlock;

                newEnemyBlock -= damageToDeal;

                if (newEnemyBlock > 0)
                {
                    damageToDeal = 0;
                }
                else
                {
                    damageToDeal = newEnemyBlock * -1;
                    newEnemyBlock = 0;
                }

                enemyScript.SetBlock(newEnemyBlock);
            }

            int newEnemyHealth = enemyStats.m_currentHP;
            newEnemyHealth -= damageToDeal;
            roundStats.DamageDealt += damageToDeal;
            totalStats.DamageDealt += damageToDeal;
            enemyScript.SetHealth(newEnemyHealth);

            if (crit)
            {
                SetInfoText("Critical Hit! " + playerStats.m_attackDamage * 2 + " Damage Dealt");
            }
            else
            {
                SetInfoText(playerStats.m_attackDamage + " Damage Dealt");
            }
                

            if (newEnemyHealth <= 0)
            {
                SetInfoText("Enemy Defeated!");
                playerScript.SetBlock(0);
                totalStats.LevelsCleared++;
                AttackButton.SetActive(false);
                BlockButton.SetActive(false);
                EndTurnButton.SetActive(false);
                ContinueButton.SetActive(true);
                m_healButton.SetActive(false);
                m_healText.SetActive(false);
                m_apTracker.SetActive(false);
            }
        }
        else
        {
            m_audioManager.PlayMiss();
            SetInfoText("Attack Missed!");
        }

        CheckAP();
    }

    public void PlayerBlock()
    {
        PlayerScript.GameplayStats playerStats = playerScript.GetStats();
        playerScript.SetBlock(playerStats.m_currentBlock + playerStats.m_blockAmount);
        SetInfoText(playerStats.m_blockAmount + " Block Gained");

        CheckAP();
    }

    public void Heal()
    {
        if (!m_healCharge)
        {
            return;
        }

        int missingHP = playerScript.GetStats().m_maxHP - playerScript.GetStats().m_currentHP;
        int toHeal = (int)Random.Range(1f, missingHP);

        playerScript.SetHealth(playerScript.GetStats().m_currentHP + toHeal);

        SetInfoText("You Healed " + toHeal + " HP");

        m_healCharge = false;
        m_healText.GetComponent<TextMeshProUGUI>().text = "0/1"; 
    }

    private void CheckAP()
    {
        playerScript.ActionUsed();
        roundStats.ActionsTaken++;
        totalStats.ActionsTaken++;

        if (playerScript.GetStats().m_currentAP <= 0 && !ContinueButton.activeSelf)
        {
            EndTurnButton.SetActive(true);
            AttackButton.SetActive(false);
            BlockButton.SetActive(false);
            m_healButton.SetActive(false);
            m_healText.SetActive(false);
            m_apTracker.SetActive(false);
        }
    }

    private void SetInfoText(string infoText)
    {
        InfoText.GetComponent<TextMeshProUGUI>().text = infoText;
    }

    public void EndTurn()
    {
        SetInfoText("Enemy Turn");
        EndTurnButton.SetActive(false);
        m_playerTurn = false;
        m_enemyActionTimer = 0f;
        enemyScript.StartTurn();
    }

    private void StartTurn()
    {
        playerScript.NewTurn();
        m_playerTurn = true;
        SetInfoText("Your Turn");
        AttackButton.SetActive(true);
        BlockButton.SetActive(true);
        m_healButton.SetActive(true);
        m_healText.SetActive(true);
        m_apTracker.SetActive(true);
        m_turn++;
        TurnText.GetComponent<TextMeshProUGUI>().text = "Turn " + m_turn;
    }

    private void EnemyDecisionMaking()
    {
        m_enemyActionTimer += Time.deltaTime;

        if (m_enemyActionTimer < 2)
        {
            if (m_enemyActionTimer > 1.5f)
            {
                SetInfoText("");
            }
            return;
        }

        m_enemyActionTimer = 0;

        EnemyScript.EnemyStats enemyStats = enemyScript.GetStats();
        PlayerScript.GameplayStats playerStats = playerScript.GetStats();

        if (enemyStats.m_currentAP <= 0)
        {
            StartTurn();
            return;
        }

        if (enemyStats.m_currentBlock < enemyStats.m_blockAmount || enemyStats.m_currentBlock < playerStats.m_attackDamage)
        {
            m_audioManager.PlayBlock();
            enemyScript.SetBlock(enemyStats.m_currentBlock + enemyStats.m_blockAmount);
            SetInfoText("Enemy Gained " + enemyStats.m_blockAmount + " Block");
            enemyScript.UseAction();
            return;
        }

        if (playerStats.m_currentBlock < enemyStats.m_attackDamage)
        {
            EnemyAttack(enemyStats, playerStats);
            enemyScript.UseAction();
            return;
        }

        if (50f > Random.Range(0.0f, 100.0f))
        {
            m_audioManager.PlayBlock();
            SetInfoText("Enemy Gained " + enemyStats.m_blockAmount + " Block");
            enemyScript.SetBlock(enemyStats.m_currentBlock + enemyStats.m_blockAmount);
        }
        else
        {
            EnemyAttack(enemyStats, playerStats);
        }

        enemyScript.UseAction();
    }

    private void EnemyAttack(EnemyScript.EnemyStats enemyStats, PlayerScript.GameplayStats playerStats)
    {
        m_enemyAnimator.SetTrigger("Attack");

        if (enemyStats.m_hitChance > Random.Range(0.0f, 100.0f))
        {
            if (playerStats.m_dodgeChance > Random.Range(0.0f, 100.0f))
            {
                m_playerAnimator.SetTrigger("Dodge");
                m_audioManager.PlayMiss();
                SetInfoText("You Dodged the Attack!");
                roundStats.AttacksDodged++;
                totalStats.AttacksDodged++;
                return;
            }

            m_playerAnimator.SetTrigger("Hit");
            m_audioManager.PlayHit();
            int damageToDeal = enemyStats.m_attackDamage;

            if (playerStats.m_currentBlock > 0)
            {
                int newPlayerBlock = playerStats.m_currentBlock;

                newPlayerBlock -= enemyStats.m_attackDamage;

                if (newPlayerBlock > 0)
                {
                    damageToDeal = 0;
                    roundStats.DamageBlocked += enemyStats.m_attackDamage;
                    totalStats.DamageBlocked += enemyStats.m_attackDamage;
                }
                else
                {
                    damageToDeal = newPlayerBlock * -1;
                    newPlayerBlock = 0;
                    roundStats.DamageBlocked += playerStats.m_currentBlock;
                    totalStats.DamageBlocked += playerStats.m_currentBlock;
                }

                playerScript.SetBlock(newPlayerBlock);
            }

            int newPlayerHealth = playerStats.m_currentHP;
            newPlayerHealth -= damageToDeal;
            playerScript.SetHealth(newPlayerHealth);
            SetInfoText("Enemy Dealt " + enemyStats.m_attackDamage + " Damage");

            if (newPlayerHealth <= 0)
            {
                LevelLost();
            }
        }
        else
        {
            m_playerAnimator.SetTrigger("Dodge");
            m_audioManager.PlayMiss();
            SetInfoText("Enemy Attack Missed!");
        }
    }
}
