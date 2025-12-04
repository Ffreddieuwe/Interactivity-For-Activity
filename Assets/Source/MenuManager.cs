using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Canvas MainMenuCanvas;
    private Canvas WorkoutSelectionCanvas;
    private Canvas LeaderboardCanvas;
    private Canvas WorkoutTrackingCanvas;
    private Canvas StepTrackingCanvas;
    private Canvas WorkoutResultCanvas;
    private Canvas InfoCanvas;
    private Canvas AboutStatsCanvas;
    private Canvas HowToPlayCanvas;
    private Canvas HelpPlanningCanvas;

    public GameObject TextPage1;
    public GameObject TextPage2;

    private List<Canvas> AllCanvas = new List<Canvas>();

    void Start()
    {
        TextPage2.SetActive(false);

        Canvas[] CanvasPlayerGUIs = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "MainMenuCanvas":
                    MainMenuCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(MainMenuCanvas);
                    break;
                case "WorkoutSelectionCanvas":
                    WorkoutSelectionCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(WorkoutSelectionCanvas);
                    break;
                case "LeaderboardCanvas":
                    LeaderboardCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(LeaderboardCanvas);
                    break;
                case "WorkoutTrackingCanvas":
                    WorkoutTrackingCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(WorkoutTrackingCanvas);
                    break;
                case "StepTrackingCanvas":
                    StepTrackingCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(StepTrackingCanvas);
                    break;
                case "WorkoutResults":
                    WorkoutResultCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(WorkoutResultCanvas);
                    break;
                case "InfoCanvas":
                    InfoCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(InfoCanvas);
                    break;
                case "AboutStatsCanvas":
                    AboutStatsCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(AboutStatsCanvas);
                    break;
                case "HowToPlayCanvas":
                    HowToPlayCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(HowToPlayCanvas);
                    break;
                case "HelpPlanningCanvas":
                    HelpPlanningCanvas = CanvasPlayerGUI;
                    AllCanvas.Add(HelpPlanningCanvas);
                    break;
            }
        }

        DisableAll();
        MainMenuCanvas.enabled = true;
    }

    private void DisableAll()
    {
        foreach (var canvas in AllCanvas)
        {
            canvas.enabled = false;
        }
    }

    public void EndWorkout()
    {
        DisableAll();
        WorkoutResultCanvas.enabled = true;
    }

    public void ReturnToMenu()
    {
        DisableAll();
        MainMenuCanvas.enabled = true;
    }

    public void StartWorkoutSelection()
    {
        DisableAll();
        WorkoutSelectionCanvas.enabled = true;
    }

    public void StartSteps()
    {
        DisableAll();
        StepTrackingCanvas.enabled = true;
    }

    public void StartWorkout()
    {
        DisableAll();
        WorkoutTrackingCanvas.enabled = true;
    }

    public void OpenInfo()
    {
        DisableAll();
        InfoCanvas.enabled = true;
    }

    public void OpenLeaderboard()
    {
        DisableAll();
        LeaderboardCanvas.enabled = true;
    }

    public void OpenAboutStats()
    {
        DisableAll();
        AboutStatsCanvas.enabled = true;
    }

    public void OpenHowToPlay()
    {
        DisableAll();
        HowToPlayCanvas.enabled = true;
    }

    public void OpenHelpPlanning()
    {
        DisableAll();
        HelpPlanningCanvas.enabled = true;
    }

    public void StartGameplay()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToggleActivePage()
    {
        TextPage1.SetActive(!TextPage1.activeInHierarchy);
        TextPage2.SetActive(!TextPage2.activeInHierarchy);
    }

    public void OpenNHSLink()
    {
        Application.OpenURL("https://www.nhs.uk/live-well/exercise/");
    }
}
