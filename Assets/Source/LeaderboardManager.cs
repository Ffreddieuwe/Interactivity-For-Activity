using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    TextMeshPro m_userFilterText;


    private void Awake()
    { 

    }

    public void ToggleUserFilter()
    {
        if (m_userFilterText.text == "World")
        {
            m_userFilterText.SetText("Friends");
        }
        else
        {
            m_userFilterText.SetText("World");
        }
    }
}
