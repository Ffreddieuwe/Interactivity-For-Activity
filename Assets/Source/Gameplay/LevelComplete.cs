using UnityEngine;
using TMPro;
using UnityEditor.EditorTools;

public class LevelComplete : MonoBehaviour
{
    public GameObject GameManager;
    private GameManager.RunStats stats = new GameManager.RunStats();

    public GameObject RemainingHPText;
    public GameObject ActionsTakenText;
    public GameObject DamageDealtText;
    public GameObject AttacksDodgedText;
    public GameObject DamageBlockedText;

    public GameObject LevelsClearedText;
    public GameObject TotalActionsText;
    public GameObject TotalDamageText;
    public GameObject TotalAttacksDodged;
    public GameObject TotalDamageBlockedText;

    public void roundWonValues()
    {
        stats = GameManager.GetComponent<GameManager>().roundStats;

        RemainingHPText.GetComponent<TextMeshProUGUI>().text = GameManager.GetComponent<PlayerScript>().GetStats().m_currentHP.ToString();
        ActionsTakenText.GetComponent<TextMeshProUGUI>().text = stats.ActionsTaken.ToString();
        DamageDealtText.GetComponent<TextMeshProUGUI>().text = stats.DamageDealt.ToString();
        AttacksDodgedText.GetComponent<TextMeshProUGUI>().text = stats.AttacksDodged.ToString();
        DamageBlockedText.GetComponent<TextMeshProUGUI>().text = stats.DamageBlocked.ToString();
    }

    public void gameOverValues()
    {
        stats = GameManager.GetComponent<GameManager>().totalStats;

        LevelsClearedText.GetComponent<TextMeshProUGUI>().text = stats.LevelsCleared.ToString();
        TotalActionsText.GetComponent<TextMeshProUGUI>().text = stats.ActionsTaken.ToString();
        TotalDamageText.GetComponent<TextMeshProUGUI>().text = stats.DamageDealt.ToString();
        TotalAttacksDodged.GetComponent<TextMeshProUGUI>().text = stats.AttacksDodged.ToString();
        TotalDamageBlockedText.GetComponent<TextMeshProUGUI>().text = stats.DamageBlocked.ToString();
    }
}
