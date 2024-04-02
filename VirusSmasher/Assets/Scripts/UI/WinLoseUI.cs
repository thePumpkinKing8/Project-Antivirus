using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public GameObject LoseMenu;
    public GameObject WinMenu;



    void Start()
    {
        LoseMenu.SetActive(false);
        WinMenu.SetActive(false);
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0; 
        Cursor.lockState = CursorLockMode.None;
        LoseMenu.SetActive(true);
    }

    public void PlayerWin()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        WinMenu.SetActive(true);
    }

}
