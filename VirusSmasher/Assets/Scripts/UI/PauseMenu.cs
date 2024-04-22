using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;



    public GameObject Checkpoint1Marker;
    public GameObject Checkpoint2Marker;
    public GameObject Checkpoint3Marker;

    public GameObject Checkpoint1Button;
    public GameObject Checkpoint2Button;
    public GameObject Checkpoint3Button;


    void Start()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Pause.triggered)  // ESC Button.
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;  // stops time in the scene.
                gamePaused = true;
                Cursor.lockState = CursorLockMode.None;  // Unlocks the mouse for the player to interact with the pause menu.


                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                gamePaused = false;
                Time.timeScale = 1;  // Time within the level continues.

            }
        }





        if (Checkpoint1Marker.activeSelf != false)
        {
            Checkpoint1Button.SetActive(true);
        }
        else
        {
            Checkpoint1Button.SetActive(false);
        }



        if (Checkpoint2Marker.activeSelf != false)
        {
            Checkpoint2Button.SetActive(true);
        }
        else
        {
            Checkpoint2Button.SetActive(false);
        }



        if (Checkpoint3Marker.activeSelf != false)
        {
            Checkpoint3Button.SetActive(true);
        }
        else
        {
            Checkpoint3Button.SetActive(false);
        }

    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }



    public void Exit()
    {
        Application.Quit();
    }

}
