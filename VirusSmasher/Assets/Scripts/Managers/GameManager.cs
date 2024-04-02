using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player;

    public Color32 color = Color.green;

    public void SetArea()
    {
        if (SceneManager.GetActiveScene().name == "PrototypeLevel")
        {
            color = Color.green;
        }
        Debug.Log(color);
        player.GetComponent<SpriteRenderer>().color = color;
    }

}
