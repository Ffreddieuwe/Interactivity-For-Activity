using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StepTracker : MonoBehaviour
{
    private bool m_IsRunning = false;
    private float m_RunTime = 0.0F;
    private int m_StepCount = 0;
    private int m_StepOffset = 0;

    public GameObject m_TimerText;
    public GameObject m_StepText;
    private ResultsManager m_ResultsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_ResultsManager = GameObject.FindFirstObjectByType(typeof(ResultsManager)) as ResultsManager;
        if (!Application.isEditor)
        {
            InputSystem.EnableDevice(StepCounter.current);
        }
    }

    public void StartWorkout()
    {
        m_IsRunning = true;
        m_RunTime = 0;
        if (!Application.isEditor)
        {
            m_StepOffset = StepCounter.current.stepCounter.ReadValue();
        }
    }

    public void TogglePauseWorkout()
    {
        m_IsRunning = !m_IsRunning;
    }


    public void EndWorkout()
    {
        m_IsRunning = false;
        m_ResultsManager.DisplayResults(1, m_RunTime, m_StepCount);
    }

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

        string timerString = hourString + ":" + minuteString + ":" + secondString;

        m_TimerText.GetComponentInChildren<TextMeshProUGUI>().text = timerString;

        if (!Application.isEditor)
        {
            m_StepCount = StepCounter.current.stepCounter.ReadValue() - m_StepOffset;
        }

        m_StepText.GetComponentInChildren<TextMeshProUGUI>().text = m_StepCount + " Steps";
    }
}
