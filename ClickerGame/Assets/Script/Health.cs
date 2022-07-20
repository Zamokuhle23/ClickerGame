using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    Points _points;


    Game _game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       _game = GameObject.FindObjectOfType<Game>();
        _points = GameObject.FindObjectOfType<Points>();

    }

    public void GetDamage(int damage)
    {


        int health = CurrentHealth - damage;

        if (health <= 0 && !_game.LastMonsterKilled)
        {
            //Destroy(_game.CurrentMonster);
            _game.monsters[_game.CurrentIndex].SetActive(false);

            MaxHealth = MaxHealth+10;
            CurrentHealth = MaxHealth;
            _game.CurrentIndex += 1;
            GameObject.FindObjectOfType<Game>().CurrentMonster.GetComponent<Animator>().SetTrigger("killed");
            StartCoroutine(_points.AwardPoints());
            if (_game.CurrentIndex == _game.monsters.Length)
            {
                _game.LastMonsterKilled = true;
                _game.win();
            }
            }
        else
        {
            CurrentHealth = health;
        }
        

    }
}
