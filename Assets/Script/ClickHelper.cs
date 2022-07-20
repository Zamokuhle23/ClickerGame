using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHelper : MonoBehaviour
{

    Game _game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _game = GameObject.FindObjectOfType<Game>();
    }

    void OnMouseDown()
    {

        if (_game.NotGameOver)
        {
            //_game.monsters[_game.CurrentIndex].SetActive(true);
            _game.CurrentMonster = _game.monsters[_game.CurrentIndex];

            GameObject.FindObjectOfType<Game>().CurrentMonster.GetComponent<Animator>().SetTrigger("Click");
            GameObject.FindObjectOfType<Game>().Player.GetComponent<Animator>().SetTrigger("Attack");

            //GetComponent<Health>().GetDamage(10);
            GameObject.FindObjectOfType<Health>().GetDamage(10);


        }
        }
    
}

