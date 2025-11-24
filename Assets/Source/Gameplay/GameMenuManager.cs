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

    public GameObject player;

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

        int hp = yourStats.Level / 1000 + baseHP;
        statsText[0].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Level/1000;
        statInfoText[0].GetComponent<TextMeshProUGUI>().text = hp + " HP";

        int ap = yourStats.Stamina / 1000 + baseAP;
        statsText[1].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stamina/1000;
        statInfoText[1].GetComponent<TextMeshProUGUI>().text = ap + " Actions per turn";

        float dodgeChance = (float)(yourStats.Agility / 1000) / 2 + baseDodgeChance;
        float hitChance = (float)(yourStats.Agility / 1000) / 2 + baseHitChance;
        statsText[2].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Strength/1000;
        statInfoText[2].GetComponent<TextMeshProUGUI>().text = dodgeChance + "% Dodge chance, " + hitChance + "% Hit chance";

        int attackDMG = yourStats.Strength / 1000 + baseAttackDamage;
        statsText[3].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Agility / 1000;
        statInfoText[3].GetComponent<TextMeshProUGUI>().text = attackDMG + " HP";

        int blockAmount = yourStats.Stability / 1000 + baseBlockAmount;
        statsText[4].GetComponent<TextMeshProUGUI>().text = "lvl." + yourStats.Stability/1000;
        statInfoText[4].GetComponent<TextMeshProUGUI>().text = blockAmount + " Block amount";

        player.GetComponent<PlayerScript>().InitStats(hp, ap, dodgeChance, hitChance, attackDMG, blockAmount);
    }
}
