using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Newtonsoft.Json;
using UnityEngine.Networking;


public class LeaderCharts : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;
    public List<HighScoreEntry> highscoreEntryList;
    

    List<GameObject> uiElements = new List<GameObject>();

    public void UpdateUI(List<HighScoreEntry> list)
    {
        SortList(list);
        for (int i = 0; i < list.Count; i++)
        {
            HighScoreEntry el = list[i];

            if (el != null && el.score > 0)
            {
                if (i >= uiElements.Count)
                {
                    // instantiate new entry
                    var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add(inst);
                }
                string rankString;
                int rank = i + 1;
                switch (rank)
                {
                    default:
                        rankString = rank + "TH";
                        break;

                    case 1:
                        rankString = "1ST";
                        break;

                    case 2:
                        rankString = "2ND";
                        break;

                    case 3:
                        rankString = "3RD";
                        break;
                }
                

                var texts = uiElements[i].GetComponentsInChildren<Text>();
                texts[0].text = rankString;
                texts[2].text = el.name;
                texts[1].text = el.score.ToString();

                HighScores highScore = new HighScores { highScoreEntryList = list };

                string json = JsonUtility.ToJson(highScore);
                PlayerPrefs.SetString("highScoreTable", json);
              
                PlayerPrefs.Save();
            }
        }
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscoreEntryList = new List<HighScoreEntry>();

        StartCoroutine(FetchLeaderboard());
     
        
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FetchLeaderboard()
    {
        string jsonString = PlayerPrefs.GetString("highScoreTable");


        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);


        if (highScore == null)
        {

            Debug.Log("No PlayerPrefs Found ");

            UnityWebRequest request = UnityWebRequest.Get("https://taptics.b-cdn.net/files/leaderboard.json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {

                Debug.Log(request.error);
            }
            else
            {

                JSONArray results = SimpleJSON.JSON.Parse(request.downloadHandler.text) as JSONArray;

                if (results == null)
                {

                    Debug.Log("........No Data.......");

                }
                else
                {


                    Debug.Log("........Json Data.......");
                    for (int i = 0; i < results.Count; i++)
                    {
                        highscoreEntryList.Add(new HighScoreEntry() { score = int.Parse(results[i][1]), name = results[i][0] });
                        //eaderBoard[i] = new HighScoreEntry(results[i][0], int.Parse(results[i][1]));
                        //ebug.Log(highscoreEntryList.Count);

                    }

                }

            }
        }
        else
        {
            Debug.Log(" PlayerPrefs Found ");
            highscoreEntryList = highScore.highScoreEntryList;
        }


    }


    public void SortList(List<HighScoreEntry> highScoreEntryList)
        {

        for (int i = 0; i < highScoreEntryList.Count; i++)
        {
            for (int j = i; j < highScoreEntryList.Count; j++)
            {
                if (highScoreEntryList[j].score > highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = tmp;

                }
            }
        }

    }
    public void ShowPanel()
    {
        table.SetActive(true);
    }

    public void ClosePanel()
    {
        table.SetActive(false);
    }
}
