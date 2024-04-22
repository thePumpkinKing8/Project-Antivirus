using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject Instructions;
    public GameObject canvas;


    [SerializeField]
    AudioSource ButtonSound;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(true);
        Instructions.SetActive(false);
        
    }


    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        Instructions.SetActive(true);

        ButtonSound.Play();
    }

    public void Exit()
    {
        Application.Quit();

        ButtonSound.Play();
    }

    public void HowToclose()
    {
        Instructions.SetActive(false);
    }


}
