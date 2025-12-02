using UnityEngine;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    private GameManager.RunStats stats = new GameManager.RunStats();

    [SerializeField]
    private AudioManager audioManager;

    // LEVEL COMPLETE SCREEN TEXT
    [SerializeField]
    private GameObject RemainingHPText;
    [SerializeField]
    private GameObject ActionsTakenText;
    [SerializeField]
    private GameObject DamageDealtText;
    [SerializeField]
    private GameObject AttacksDodgedText;
    [SerializeField]
    private GameObject DamageBlockedText;

    // GAME OVER SCREEN TEXT
    [SerializeField]
    private GameObject LevelsClearedText;
    [SerializeField]
    private GameObject TotalActionsText;
    [SerializeField]
    private GameObject TotalDamageText;
    [SerializeField]
    private GameObject TotalAttacksDodged;
    [SerializeField]
    private GameObject TotalDamageBlockedText;

    public void roundWonValues()
    {
        audioManager.PlayLevelComplete();
        stats = GameManager.GetComponent<GameManager>().roundStats;

        RemainingHPText.GetComponent<TextMeshProUGUI>().text = GameManager.GetComponent<PlayerScript>().GetStats().m_currentHP.ToString();
        ActionsTakenText.GetComponent<TextMeshProUGUI>().text = stats.ActionsTaken.ToString();
        DamageDealtText.GetComponent<TextMeshProUGUI>().text = stats.DamageDealt.ToString();
        AttacksDodgedText.GetComponent<TextMeshProUGUI>().text = stats.AttacksDodged.ToString();
        DamageBlockedText.GetComponent<TextMeshProUGUI>().text = stats.DamageBlocked.ToString();
    }

    public void gameOverValues()
    {
        audioManager.PlayLevelLost();
        stats = GameManager.GetComponent<GameManager>().totalStats;

        LevelsClearedText.GetComponent<TextMeshProUGUI>().text = stats.LevelsCleared.ToString();
        TotalActionsText.GetComponent<TextMeshProUGUI>().text = stats.ActionsTaken.ToString();
        TotalDamageText.GetComponent<TextMeshProUGUI>().text = stats.DamageDealt.ToString();
        TotalAttacksDodged.GetComponent<TextMeshProUGUI>().text = stats.AttacksDodged.ToString();
        TotalDamageBlockedText.GetComponent<TextMeshProUGUI>().text = stats.DamageBlocked.ToString();
    }
}
