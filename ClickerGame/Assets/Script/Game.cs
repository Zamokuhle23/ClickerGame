using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections.Generic;


public class Game : MonoBehaviour
{

    public Text TimeLeft;
    public Transform startPosition;
    //public Button ContinueButn;
    public Button RestartButn;
    Admob _admob;
    //public LeaderBoard[] leaderBoard = new LeaderBoard[8];
    public GameObject[] monsters;
    public GameObject Player;
    public GameObject CurrentMonster;
    public int CurrentIndex = 0;
    public bool LastMonsterKilled = false;
    public float timeLeft = 60f;
    public Text Username;
    public String Name;
    public Text GameStatus;
    public Text ShowPoints;
    public Text MonstersLeft;
    public Slider mySlider;
    public Health _health;
    public bool NotGameOver = true;
    public Image image;
    public Image Defeat;
    LeaderCharts _leaderCharts;
    Points _points;




    void Start()
    {
        //PlayerPrefs.DeleteAll();
        _health = GameObject.FindObjectOfType<Health>();
        _admob = GameObject.FindObjectOfType<Admob>();
        _leaderCharts = GameObject.FindObjectOfType<LeaderCharts>();
        _points = GameObject.FindObjectOfType<Points>();
        


        Username.text = PlayerPrefs.GetString("username");

        Name = Username.text.ToString();




        if (Name.Length == 0)
        {
            NotGameOver = false;
            image.gameObject.SetActive(true);
        }

    }
       



    void Update()
    {
        TimeLeft.text = "Time Left : " + (int)Math.Round(timeLeft);
        mySlider.maxValue = _health.MaxHealth;
        mySlider.value = _health.CurrentHealth;
        Username.text = PlayerPrefs.GetString("username");
        MonstersLeft.text = "Monsters Left: " + (10 - CurrentIndex).ToString();

        if (CurrentIndex < monsters.Length)
        {
            monsters[CurrentIndex].SetActive(true);
        }

        if(LastMonsterKilled)
        {
            NotGameOver = false;
            
            RestartButn.gameObject.SetActive(true);
        }
        else
        {
            if(NotGameOver)
              timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                GameOver();
                _admob.BtnReward.gameObject.SetActive(true);
                RestartButn.gameObject.SetActive(true);
                NotGameOver = false;
                timeLeft = 0;
            }
        }

    }

    public void restart()
    {
        CurrentIndex = 0;
        _health.CurrentHealth = 100;
        _health.MaxHealth = 100;
        _admob.BtnReward.gameObject.SetActive(false);
        RestartButn.gameObject.SetActive(false);
        Defeat.gameObject.SetActive(false);
        NotGameOver = true;
        _leaderCharts.ClosePanel();
    }




        public void Continue()
    {
        Defeat.gameObject.SetActive(false);
        
        _admob.BtnReward.gameObject.SetActive(false);
        RestartButn.gameObject.SetActive(false);
        NotGameOver = true;

    }

    public void win()
    {
        
        _leaderCharts.SortList(_leaderCharts.highscoreEntryList);

        //GameStatus.gameObject.SetActive(true);
        if (_points.SumPoints > _leaderCharts.highscoreEntryList[7].score)
        {
           // Debug.Log(timeLeft + " " + _leaderCharts.highscoreEntryList[7].score + " Called");
            _leaderCharts.highscoreEntryList.RemoveAt(_leaderCharts.highscoreEntryList.Count - 1);
       

          _leaderCharts.highscoreEntryList.Add(new HighScoreEntry() { score = _points.SumPoints, name = Name });


        }
        _leaderCharts.UpdateUI(_leaderCharts.highscoreEntryList);
        _leaderCharts.ShowPanel();
        int n = 60 - (int)Math.Round(timeLeft);
        ShowPoints.text = "Total Points: " + (_points.SumPoints).ToString() + "\n" + "Time Taken: " + n.ToString();

    }

        void GameOver()
    {
        //GameStatus.gameObject.SetActive(true);
        Defeat.gameObject.SetActive(true);
        GameStatus.text = "GAME OVER \n YOU LOST";
        //SceneManager.LoadScene(2);
    }

}
