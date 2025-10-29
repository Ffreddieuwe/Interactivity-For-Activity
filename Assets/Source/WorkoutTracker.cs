using TMPro;
using UnityEngine;

public class WorkoutTracker : MonoBehaviour
{
    private bool m_IsRunning = false;
    private float m_RunTime = 0.0F;
    private int m_WorkoutType = -1;


    private TMP_Text m_TimerText;
    private ResultsManager m_ResultsManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TimerText = GetComponentInChildren<TMP_Text>();
        m_ResultsManager = GameObject.FindFirstObjectByType(typeof(ResultsManager)) as ResultsManager;
    }

    public void StartWorkout(int workoutType)
    {
        m_IsRunning = true;
        m_RunTime = 0;
        m_WorkoutType = workoutType;
    }

    public void TogglePauseWorkout()
    {
        m_IsRunning = !m_IsRunning;
    }

    public void EndWorkout()
    {
        m_IsRunning = false;
        m_ResultsManager.DisplayResults(m_WorkoutType, m_RunTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsRunning)
        {
            return;
        }

        m_RunTime += Time.deltaTime;

        int seconds = (int)m_RunTime % 60;
        string secondString = seconds < 10 ? "0" + seconds : "" + seconds;
        float minutes = Mathf.Floor(m_RunTime / 60);
        minutes = minutes % 60;
        string minuteString = minutes < 10 ? "0" + minutes : "" + minutes;
        float hours = Mathf.Floor(minutes / 60);
        string hourString = hours < 10 ? "0" + hours : "" + hours;

        string timerString = hourString+ ":"+minuteString+":"+secondString;

        m_TimerText.text = timerString;
    }
}
