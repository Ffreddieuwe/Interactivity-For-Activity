using TMPro;
using UnityEngine;
using SimpleJSON;

public class ResultsManager : MonoBehaviour
{
    private TMP_Text[] allText;
    private JSONHandler jsonhandler;
    public GameObject[] progressBars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allText = GetComponentsInChildren<TMP_Text>();
    }

    public void DisplayResults(int exerciseType, float duration, int steps = 0)
    {
        int seconds = (int)duration % 60;
        string secondString = seconds < 10 ? "0" + seconds : "" + seconds;
        float minutes = Mathf.Floor(duration / 60);
        minutes = minutes % 60;
        string minuteString = minutes < 10 ? "0" + minutes : "" + minutes;
        float hours = Mathf.Floor(minutes / 60);
        string hourString = hours < 10 ? "0" + hours : "" + hours;

        string timerString = hourString + ":" + minuteString + ":" + secondString;

        allText[1].text = "Workout Duration: " + timerString;

        allText[2].text = steps > 0 ? "Steps Counted: " + steps : "";

        jsonhandler = GameObject.FindFirstObjectByType(typeof(JSONHandler)) as JSONHandler;

        JSONHandler.PlayerData currentData = jsonhandler.playerDataList.playerData[0];

        //INSERT PROPAH LEVELLING UP HERE

        currentData.Level = 2200;
        currentData.Stamina = 1200;
        currentData.Agility = 1000;
        currentData.Strength = 1000;
        currentData.Stability = 1400;

        allText[3].text = "Level: " + currentData.Level/1000;
        progressBars[0].GetComponent<RectTransform>().localScale = new Vector3((currentData.Level % 1000) / 1000f, 1, 1);
        allText[4].text = "Stamina: " + currentData.Stamina / 1000;
        progressBars[1].GetComponent<RectTransform>().localScale = new Vector3((currentData.Stamina % 1000) / 1000f, 1, 1);
        allText[5].text = "Agility: " + currentData.Agility / 1000;
        progressBars[2].GetComponent<RectTransform>().localScale = new Vector3((currentData.Agility % 1000) / 1000f, 1, 1);
        allText[6].text = "Strength: " + currentData.Strength / 1000;
        progressBars[3].GetComponent<RectTransform>().localScale = new Vector3((currentData.Strength % 1000) / 1000f, 1, 1);
        allText[7].text = "Stability: " + currentData.Stability / 1000;
        progressBars[4].GetComponent<RectTransform>().localScale = new Vector3((currentData.Stability % 1000) / 1000f, 1, 1);

        jsonhandler.playerDataList.playerData[0] = currentData;
        jsonhandler.UpdateJSON();
    }
}
