/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using NaughtyAttributes;

public class HighScore : MonoBehaviour
{

    public Transform entryContainer;
    public Transform entryTemplate;

    public int maxEntryCount = 8;
    public float entryPlacementDifference;

    public Color firstPlaceColour;

    private List<Transform> highscoreEntryTransformList;
    public List<HighscoreEntry> highscoreEntryList;

    public void SortPlayers(LeaderBoard[] leaderBoard)
    {
        for (int i = 1; i <= results.Count; i++)
        {
            for (int j = 1; j < results.Count - 1; j++)
            {
                if (leaderBoard[j].score < leaderBoard[j + 1].score)
                {
                    LeaderBoard temp = leaderBoard[j];
                    leaderBoard[j] = leaderBoard[j + 1];
                    leaderBoard[j + 1] = temp;
                }
            }
        }

    }

    private void Start()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = transform.Find(highscoreEntryTemplate");


        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>()
        {
            new HighscoreEntry{score = 40 , name = "Zama",
            new HighscoreEntry{score = 40 , name = "Sbu
            new HighscoreEntry{score = 40 , name = "Jack
            new HighscoreEntry{score = 40 , name = "Masondo
            new HighscoreEntry{score = 40 , name = "Dez
            new HighscoreEntry{score = 40 , name = "Sello
            new HighscoreEntry{score = 40 , name = "Mua

            new HighscoreEntry{score = 40 , name = "Zama",

        }

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);

        highscoreEntryTransformList = new List<Transform>();

        // Simple sorting algorithm - for each element you cycle thru each element under that one
        // Then you test to see which one has the higher score, and if needed swap them in the list
        for (int i = 0; i < highScore.highScoreEntryList.Count; i++)
        {
            for (int j = i; j < highScore.highScoreEntryList.Count; j++)
            {
                if (highScore.highScoreEntryList[j].score > highScore.highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScore.highScoreEntryList[i];
                    highScore.highScoreEntryList[i] = highScore.highScoreEntryList[j];
                    highScore.highScoreEntryList[j] = tmp;

                }
            }
        }


        // Limits score entries
        if (highScore.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScore.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScore.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        foreach (HighScoreEntry highScoreEntry in highScore.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    IEnumerator FetchLeaderboard()
    {
        Debug.Log("star ");

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
                    leaderBoard[i] = new LeaderBoard(results[i][0], int.Parse(results[i][1]));

                }


            }



        }

        //Debug.Log("Not Error");




    }



    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -entryPlacementDifference * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

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

        entryTransform.Find("Position").GetComponent<Text>().text = rankString;
        entryTransform.Find("Score").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entryTransform.Find("Name").GetComponent<Text>().text = highscoreEntry.name;

        // Set background to alternate
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        // Set fancy first place colour
        if (rank == 1)
        {
            entryTransform.Find("Position").GetComponent<Text>().color = firstPlaceColour;
            entryTransform.Find("Score").GetComponent<Text>().color = firstPlaceColour;
            entryTransform.Find("Name").GetComponent<Text>().color = firstPlaceColour;

        }

        transformList.Add(entryTransform);
    }

    public void AddHighScoreEntry(int score, string name)
    {
        // Create HighScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScoreTmp = JsonUtility.FromJson<HighScores>(jsonString);

        // Limits score entries
        if (highScoreTmp != null && highScoreTmp.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScoreTmp.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScoreTmp.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        // Load saved HighScores
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);

        // Add new entry to HighscoreTable
        if (highScore == null)
        {
            var highScoreFirstEntry = new List<HighScoreEntry>() { highScoreEntry };
            highScore = new HighScores { highScoreEntryList = highScoreFirstEntry };
        }
        else
        {
            // Add new entry to list
            highScore.highScoreEntryList.Add(highScoreEntry);
        }


        // Save updated list
        string json = JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


}
*/