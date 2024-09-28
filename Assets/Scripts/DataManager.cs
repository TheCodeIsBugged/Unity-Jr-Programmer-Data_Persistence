using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public TMP_InputField inputPlayerName;

    public string currentPlayer;
    public string bestPlayer;
    public int bestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayerData();
    }

    private void Update()
    {
        if (inputPlayerName != null)
        {
            currentPlayer = inputPlayerName.text;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string CurrentPlayerName;
        public string BestPlayerName;
        public int BestPlayerScore;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.CurrentPlayerName = currentPlayer;
        data.BestPlayerName = bestPlayer;
        data.BestPlayerScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath  + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = new SaveData();
            data = JsonUtility.FromJson<SaveData>(json);

            currentPlayer = data.CurrentPlayerName;
            bestPlayer = data.BestPlayerName;
            bestScore = data.BestPlayerScore;
        }
        else
        {
            bestPlayer = "null";
            bestScore = 0;
        }
    }
}
