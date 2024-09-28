using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<BestScoreEntry> bestScoreEntries;
    public BestScoreEntry newPlayerEntry;
    
    void Awake()
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

        LoadBestScoreList();        
    }

    [System.Serializable]
    public class BestScoreList
    {
        public List<BestScoreEntry> BestScoreEntries;
    }

    [System.Serializable]
    public class BestScoreEntry
    {
        public string Name;
        public int Score;
    }

    public void SaveBestScoreList()
    {
        BestScoreList bestScoreList = new BestScoreList();
        bestScoreList.BestScoreEntries = bestScoreEntries;

        string json = JsonUtility.ToJson(bestScoreList);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadBestScoreList()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreList data = JsonUtility.FromJson<BestScoreList>(json);

            bestScoreEntries = data.BestScoreEntries;
            SortBestScoreList(bestScoreEntries);
        }
    }

    public void AddBestScoreEntry(string name, int score, List<BestScoreEntry> bestScoreList)
    {
        BestScoreEntry newBestScoreEntry = new BestScoreEntry();
        newBestScoreEntry.Name = name;
        newBestScoreEntry.Score = score;

        bestScoreList.Add(newBestScoreEntry);

        SaveBestScoreList();
    }

    public void SortBestScoreList(List<BestScoreEntry> bestScoreList)
    {
        for (int i = 0; i < bestScoreList.Count; i++)
        {
            for (int j = i + 1; j < bestScoreList.Count; j++)
            {
                if (bestScoreList[j].Score > bestScoreList[i].Score)
                {
                    BestScoreEntry temp = bestScoreList[i];
                    bestScoreList[i] = bestScoreList[j];
                    bestScoreList[j] = temp;
                }
            }
        }

        if (bestScoreList.Count > 5)
        {
            bestScoreList.RemoveAt(bestScoreList.Count - 1);
        }
    }
}
