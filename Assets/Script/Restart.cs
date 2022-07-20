using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public Health _health;
    public Game _game;

    // Start is called before the first frame update
    void Start()
    {
        _health = GameObject.FindObjectOfType<Health>();
        _game = GameObject.FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        _game.CurrentIndex = 0;
        _health.CurrentHealth = 100;
        _health.MaxHealth = 100;
        SceneManager.LoadScene(0);
    }
}
