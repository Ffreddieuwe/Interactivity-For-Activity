using System;
using System.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject Userfilter;
    public GameObject Statfilter;
    public int selectedStat;
    public string selectedStatName;
    private int userIndex;

    private JSONHandler jsonhandler;
    private JSONHandler.PlayerData[] allPlayerData;

    public GameObject[] LeaderboardSlots;
    public GameObject YourStats;

    private SortClass[] sortElements;

    class SortClass
    {
        public string name;
        public int statLevel;
    }

    private void Start()
    {
        jsonhandler = GameObject.FindFirstObjectByType(typeof(JSONHandler)) as JSONHandler;
    }

    public void SetupLeaderBoard()
    {
        Userfilter.GetComponentInChildren<TextMeshProUGUI>().text = "World";
        Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Level";
        selectedStat = 0;
        userIndex = 15;

        selectedStatName = "Level";

        allPlayerData = jsonhandler.playerDataList.playerData;

        populateSortElements();
    }

    public void ToggleUserFilter()
    {
        if (Userfilter.GetComponentInChildren<TextMeshProUGUI>().text == "World")
        {
            Userfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Friends";
            userIndex = 10;
        }
        else
        {
            Userfilter.GetComponentInChildren<TextMeshProUGUI>().text = "World";
            userIndex = 15;
        }

        populateSortElements();
    }

    public void ToggleStatFilter()
    {
        switch (selectedStat)
        {
            case 0:
                selectedStat = 1;
                Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Stamina";
                selectedStatName = "Stamina";
                break;
            case 1:
                selectedStat = 2;
                Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Agility";
                selectedStatName = "Agility";
                break;
            case 2:
                selectedStat = 3;
                Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Strength";
                selectedStatName = "Strength";
                break;
            case 3:
                selectedStat = 4;
                Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Stability";
                selectedStatName = "Stability";
                break;
            case 4:
                selectedStat = 0;
                Statfilter.GetComponentInChildren<TextMeshProUGUI>().text = "Level";
                selectedStatName = "Level";
                break;

        }

        populateSortElements();
    }

    private void BubbleSort()
    {
        SortClass temp;

        for (int write = 0; write < sortElements.Length; write++)
        {
            for (int sort = 0; sort < sortElements.Length - 1; sort++)
            {
                if (sortElements[sort].statLevel < sortElements[sort + 1].statLevel)
                {
                    temp = sortElements[sort + 1];
                    sortElements[sort + 1] = sortElements[sort];
                    sortElements[sort] = temp;
                }
            }
        }
    }

    private void populateSortElements()
    {
        sortElements = new SortClass[userIndex];

        for (int i = 0; i < userIndex; i++)
        {
            SortClass sortElement = new SortClass();
            sortElement.name = allPlayerData[i].Name;

            switch (selectedStatName)
            {
                case "Level":
                    sortElement.statLevel = allPlayerData[i].Level;
                    break;
                case "Stamina":
                    sortElement.statLevel = allPlayerData[i].Stamina;
                    break;
                case "Agility":
                    sortElement.statLevel = allPlayerData[i].Agility;
                    break;
                case "Strength":
                    sortElement.statLevel = allPlayerData[i].Strength;
                    break;
                case "Stability":
                    sortElement.statLevel = allPlayerData[i].Stability;
                    break;
            }

            sortElements[i] = sortElement;
        }

        BubbleSort();

        for (int i = 0; i < 10; i++)
        {
            LeaderboardSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1) + ". " + sortElements[i].name + ": " + selectedStatName + " " + sortElements[i].statLevel / 1000;
        }

        int playerIndex;

        for (playerIndex = 0; playerIndex < sortElements.Length; playerIndex++)
        {
            if (sortElements[playerIndex].name == "You")
            {
                break;
            }
        }

        YourStats.GetComponentInChildren<TextMeshProUGUI>().text = (playerIndex + 1) + ". You: " + selectedStatName + " " + allPlayerData[0].Level / 1000;
    }
}
