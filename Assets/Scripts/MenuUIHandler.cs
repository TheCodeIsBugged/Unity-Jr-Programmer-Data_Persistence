using System.Collections;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    // public TextMeshProUGUI bestRecord;
    private List<Transform> enteriesTranform = new List<Transform>();

    public GameObject bestScoreTable;
    public Transform entryContainer;
    public Transform entryTemplate;

    public TMP_InputField inputPlayerName;

    void Awake()
    {

        
    }

    void Start()
    {
        foreach (GameManager.BestScoreEntry entry in GameManager.Instance.bestScoreEntries)
        {
            CreateBestScoreEntry(entry, entryContainer, enteriesTranform);
        }
    }

    void Update()
    {
        GameManager.Instance.newPlayerEntry.Name = inputPlayerName.text;
    }

    public void CreateBestScoreEntry(GameManager.BestScoreEntry bestScoreEntry, Transform entryContainer, List<Transform> entriesTransform)
    {
        float templateHeight = 25f;

        Transform entryInstantiate = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryInstantiate.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * entriesTransform.Count);

        entryInstantiate.gameObject.SetActive(true);

        int rank = entriesTransform.Count + 1;
        string rankName;
        switch (rank)
        {
            case 1:
                rankName = "1ST";
                break;
            case 2:
                rankName = "2ND";
                break;
            case 3:
                rankName = "3RD";
                break;
            default:
                rankName = rank.ToString() + "TH";
                break;
        }

        entryInstantiate.Find("RankText").GetComponent<TextMeshProUGUI>().text = rankName;
        entryInstantiate.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = bestScoreEntry.Score.ToString();
        entryInstantiate.Find("NameText").GetComponent<TextMeshProUGUI>().text = bestScoreEntry.Name;

        entriesTransform.Add(entryInstantiate);

        if (rank % 2 != 1)
        {
            entryInstantiate.Find("Background-BestScoreEntry").gameObject.SetActive(false);
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitMenuScene()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OpenBestScoreTable()
    {
        bestScoreTable.SetActive(true);
    }

    public void CloseBestScoreTable()
    {
        bestScoreTable.SetActive(false);
    }
}
