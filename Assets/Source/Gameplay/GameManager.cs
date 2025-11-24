using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.EditorTools;

public class GameManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject GameplayScreen;
    public GameObject LevelCompleteScreen;
    public GameObject GameOverScreen;

    private PlayerScript playerScript;

    public GameObject APText;
    public GameObject PlayerBlockText;
    public GameObject PlayerHPText;
    public GameObject EnemyBlockText;
    public GameObject EnemyHPText;
    public GameObject TurnText;

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
    }

    public void LevelWon()
    {
        DisableAll();
        LevelCompleteScreen.SetActive(true);
    }

    public void LevelLost() 
    { 
        DisableAll();
        GameOverScreen.SetActive(true);
    }

    public void ReturnToStart()
    {
        DisableAll();
        StartScreen.SetActive(true);
    }

    private void DisableAll()
    {
        StartScreen.SetActive(false);
        GameplayScreen.SetActive(false);
        LevelCompleteScreen.SetActive(false);
        GameOverScreen.SetActive(false);
    }
}
