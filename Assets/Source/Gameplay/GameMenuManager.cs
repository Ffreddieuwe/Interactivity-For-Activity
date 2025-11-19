using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject[] statsText;
    public GameObject[] statInfoText;

    public int baseHP = 20;
    public int baseAP = 2;
    public float baseHitChance = 50.0f;
    public float baseDodgeChance = 10.0f;
    public int baseAttackDamage = 5;
    public int baseBlockAmount = 5;

    private JSONHandler jsonhandler;
    private JSONHandler.PlayerData yourStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jsonhandler = GameObject.FindFirstObjectByType(typeof(JSONHandler)) as JSONHandler;
        DisplayStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayStats()
    {
        jsonhandler.ReadJSON();
        yourStats = jsonhandler.playerDataList.playerData[0];

        statsText[0].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Level/1000;
        statInfoText[0].GetComponent<TextMeshProUGUI>().text = (yourStats.Level / 1000 + baseHP) + " HP";

        statsText[1].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stamina/1000;
        statInfoText[1].GetComponent<TextMeshProUGUI>().text = (yourStats.Stamina / 1000 + baseAP) + " Actions per turn";

        statsText[2].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Agility/1000;
        statInfoText[2].GetComponent<TextMeshProUGUI>().text = (yourStats.Level / 1000 + baseHP) + " HP";

        statsText[3].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Strength/1000;
        statInfoText[3].GetComponent<TextMeshProUGUI>().text = ((float)(yourStats.Agility / 1000) / 2 + baseDodgeChance).ToString() + "% Dodge chance, " + ((float)(yourStats.Agility / 1000) / 2 + baseHitChance).ToString() + "% Hit chance";

        statsText[4].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stability/1000;
        statInfoText[4].GetComponent<TextMeshProUGUI>().text = (yourStats.Stability / 1000 + baseBlockAmount) + " Block amount";
    }
}
