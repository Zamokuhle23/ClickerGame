using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
   
    float awardTime = 2f;
    float elseTime = 2f;
    public Text points;
    public Text Totalpoints;
    public int SumPoints = 0;
    float calcTime = 0f;
    float TimeTaken = 60f;
    Game _game;
    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator AwardPoints()
    {
        awardTime = awardTime + 0.1f;
        elseTime = elseTime + 0.2f;

        calcTime = TimeTaken - _game.timeLeft;
        TimeTaken = _game.timeLeft;
        
        if (calcTime < awardTime)
        {
            points.text = "+10";
            SumPoints += 10;
        }
        else if(calcTime < elseTime)
        {
            points.text = "+6";
            SumPoints += 6;
        }
        else
        {
            points.text = "+4";
            SumPoints += 4;
        }
        Debug.Log(calcTime + " Points Awarded " + points.text.ToString());
        Debug.Log(awardTime);
        Debug.Log(elseTime);

        Totalpoints.text = SumPoints.ToString() + " Points";

        points.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        points.gameObject.SetActive(false);

    }
}
