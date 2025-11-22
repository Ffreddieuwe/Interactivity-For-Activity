using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject GameplayScreen;
    public GameObject LevelCompleteScreen;
    public GameObject GameOverScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAll();
        StartScreen.SetActive(true);
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
