using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.EditorTools;

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
    public GameObject LevelComplete;
    //

    // UI ELEMENTS
    public GameObject APText;
    public GameObject PlayerBlockText;
    public GameObject PlayerHPText;
    public GameObject EnemyBlockText;
    public GameObject EnemyHPText;
    public GameObject TurnText;
    public GameObject InfoText;
    //

    private PlayerScript playerScript;

    public int turn;
    public bool playerTurn;

    public int TEMP_EnemyBlock;
    public int TEMP_EnemyHP;
    public int TEMP_EnemyMaxHP;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAll();
        StartScreen.SetActive(true);
        playerScript = GetComponentInParent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel()
    {
        DisableAll();
        GameplayScreen.SetActive(true);

        APText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_currentAP + "/" + playerScript.GetStats().m_maxAP;
        PlayerBlockText.GetComponent<TextMeshProUGUI>().text = "0";
        PlayerHPText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_currentHP + "/" + playerScript.GetStats().m_maxHP;

        TEMP_EnemyHP = 10;
        TEMP_EnemyMaxHP = 10;
        EnemyHPText.GetComponent<TextMeshProUGUI>().text = TEMP_EnemyHP.ToString();
        EnemyBlockText.GetComponent<TextMeshProUGUI>().text = "0";
        TEMP_EnemyBlock = 10;
        EnemyBlockText.GetComponent<TextMeshProUGUI>().text = TEMP_EnemyBlock.ToString();

        InfoText.GetComponent<TextMeshProUGUI>().text = "Your Turn";
        playerTurn = true;

        roundStats = new RunStats();
    }

    public void LevelWon()
    {
        LevelComplete.GetComponent<LevelComplete>().roundWonValues();
        DisableAll();
        LevelCompleteScreen.SetActive(true);
    }

    public void LevelLost() 
    {
        LevelComplete.GetComponent<LevelComplete>().gameOverValues();
        DisableAll();
        GameOverScreen.SetActive(true);
    }

    public void ReturnToStart()
    {
        DisableAll();
        StartScreen.SetActive(true);
        roundStats = new RunStats();
        totalStats = new RunStats();
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
        if (!playerTurn)
        {
            return;
        }

        if (playerScript.GetStats().m_hitChance > Random.Range(0.0f, 100.0f))
        {
            int damageToDeal = playerScript.GetStats().m_attackDamage;

            if (TEMP_EnemyBlock > 0)
            {
                TEMP_EnemyBlock -= playerScript.GetStats().m_attackDamage;

                if (TEMP_EnemyBlock > 0)
                {
                    damageToDeal = 0;
                }
                else
                {
                    damageToDeal = TEMP_EnemyBlock * -1;
                    TEMP_EnemyBlock = 0;
                }

                EnemyBlockText.GetComponent<TextMeshProUGUI>().text = TEMP_EnemyBlock.ToString();
            }

            TEMP_EnemyHP -= damageToDeal;
            EnemyHPText.GetComponent<TextMeshProUGUI>().text = TEMP_EnemyHP.ToString();
            roundStats.DamageDealt += damageToDeal;
            totalStats.DamageDealt += damageToDeal;

            InfoText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_attackDamage + " Damage Dealt";

            if (TEMP_EnemyHP <= 0)
            {
                playerScript.currentBlock = 0;
                totalStats.LevelsCleared++;
                LevelWon();
            }
        }
        else
        {
            InfoText.GetComponent<TextMeshProUGUI>().text = "Attack Missed!";
        }

        playerScript.ActionUsed();
        roundStats.ActionsTaken++;
        totalStats.ActionsTaken++;
        APText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_currentAP + "/" + playerScript.GetStats().m_maxAP;

        if (playerScript.GetStats().m_currentAP <= 0)
        {
            playerTurn = false;
            InfoText.GetComponent<TextMeshProUGUI>().text = "Enemy Turn";
        }
    }

    public void PlayerBlock()
    {
        if (!playerTurn)
        {
            return;
        }

        playerScript.currentBlock += playerScript.GetStats().m_blockAmount;
        PlayerBlockText.GetComponent<TextMeshProUGUI>().text = playerScript.currentBlock.ToString();
        playerScript.ActionUsed();
        roundStats.ActionsTaken++;
        totalStats.ActionsTaken++;
        APText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_currentAP + "/" + playerScript.GetStats().m_maxAP;
        InfoText.GetComponent<TextMeshProUGUI>().text = playerScript.GetStats().m_blockAmount + " Block Gained";

        if (playerScript.GetStats().m_currentAP <= 0)
        {
            playerTurn = false;
            InfoText.GetComponent<TextMeshProUGUI>().text = "Enemy Turn";
        }
    }
}
