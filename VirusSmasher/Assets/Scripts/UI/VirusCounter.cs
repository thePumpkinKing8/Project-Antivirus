using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirusCounter : MonoBehaviour
{
    private int Virusleft = 3;
    public TextMeshProUGUI scoreText;

    protected WinLoseUI WinLoseui;

    void Start()
    {
        //Timer.instence.BeginTimer();  // Activates Timer code upon level start.

        
    }

    // Update is called once per frame
    void Update()
    {
        this.scoreText.text = String.Format("{0} Viruses Remaining", Virusleft);
    }
    

    public void UpdateScore(int increment)
    {
        this.Virusleft -= increment;

        if (Virusleft == 0)  // Once all Viruses have been destroyed this code gets triggered.
        {
            //Timer.instence.EndTimer();

            Time.timeScale = 0;

            WinLoseui.PlayerWin();

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);  // Code set to the side to be acessed by the pause menu button to reload the level.
    }
}
