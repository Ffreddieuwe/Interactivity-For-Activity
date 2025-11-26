using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject[] statsText;
    public GameObject[] statInfoText;

    public GameObject m_player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player.GetComponent<PlayerScript>().InitStats();

        PlayerScript.GameplayStats playerStats = m_player.GetComponent<PlayerScript>().GetStats();
        JSONHandler.PlayerData yourStats = m_player.GetComponent<PlayerScript>().yourStats;

        statsText[0].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Level / 1000;
        statInfoText[0].GetComponent<TextMeshProUGUI>().text = playerStats.m_maxHP + " HP";

        statsText[1].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stamina / 1000;
        statInfoText[1].GetComponent<TextMeshProUGUI>().text = playerStats.m_maxAP + " Actions per turn";

        statsText[2].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Agility / 1000;
        statInfoText[2].GetComponent<TextMeshProUGUI>().text = playerStats.m_dodgeChance + "% Dodge chance, " + playerStats.m_hitChance + "% Hit chance";

        statsText[3].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Strength / 1000;
        statInfoText[3].GetComponent<TextMeshProUGUI>().text = playerStats.m_attackDamage + " Attack damage";

        statsText[4].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stability / 1000;
        statInfoText[4].GetComponent<TextMeshProUGUI>().text = playerStats.m_blockAmount + " Block amount";
    }
}
