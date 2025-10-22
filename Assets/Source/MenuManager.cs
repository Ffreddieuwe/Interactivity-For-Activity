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

    private List<Canvas> AllCanvas = new List<Canvas>();

    void Start()
    {
        Canvas[] CanvasPlayerGUIs = FindObjectsOfType<Canvas>();
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

    public void OpenLeaderboard()
    {
        DisableAll();
        LeaderboardCanvas.enabled = true;
    }

    public void StartGameplay()
    {
        SceneManager.LoadScene("Game");
    }
}
