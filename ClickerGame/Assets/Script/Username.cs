using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{

    public InputField username;


    public void SaveUsername()
    {
        PlayerPrefs.SetString("username", username.text);
        GameObject.FindObjectOfType<Game>().Name = username.text.ToString();
        GameObject.FindObjectOfType<Game>().image.gameObject.SetActive(false);
        GameObject.FindObjectOfType<Game>().NotGameOver = true;
    }
}
