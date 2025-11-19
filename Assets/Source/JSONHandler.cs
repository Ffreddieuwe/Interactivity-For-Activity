using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONHandler : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class PlayerData
    {
        public string Name;
        public int Level;
        public int Stamina;
        public int Agility;
        public int Strength;
        public int Stability;
    }

    [System.Serializable]
    public class PlayerDataList
    {
        public PlayerData[] playerData;
    }

    public PlayerDataList playerDataList = new PlayerDataList();

    private void Start()
    {
        ReadJSON();
    }

    public void ReadJSON()
    {
        playerDataList = JsonUtility.FromJson<PlayerDataList>(textJSON.text);
    }

    public void UpdateJSON()
    {
        string strOutput = JsonUtility.ToJson(playerDataList);
        if (!Application.isEditor)
        {
            string dataPath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
            File.WriteAllText(dataPath, strOutput);
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/Data/PlayerData.json", strOutput);
        }
    }
}
