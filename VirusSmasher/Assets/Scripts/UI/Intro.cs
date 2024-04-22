using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    private Coroutine TypingCorutine;


    private float TypeSpeed = 20f;

    private float sentencedelay = 1.5f;

    private float finishedelay = 5.0f;



    private int CurrentLetterIndex;


    private WaitForSeconds Delay;
    private WaitForSeconds SentenceDelay;
    private WaitForSeconds FinishDelay;



    public TMP_Text IntroText;

    [SerializeField] private string TextTest;


    [SerializeField]
    AudioSource TypeNoise;


    private void Awake()
    {

        IntroText = GetComponent<TMP_Text>();


        Delay = new WaitForSeconds(1 / TypeSpeed);   

        SentenceDelay = new WaitForSeconds(sentencedelay);

        FinishDelay = new WaitForSeconds(finishedelay);
    }

    private void Start()
    {
        SetText(TextTest);
    }


    private void SetText (string text)
    {

        //if (TypingCorutine != null)
            //StopCoroutine(TypingCorutine);

        IntroText.text = text;

        IntroText.maxVisibleCharacters = 0;

        CurrentLetterIndex = 0;

        TypingCorutine = StartCoroutine(routine: Writing());
    }


    private IEnumerator Writing()
    {
         TMP_TextInfo textInfo = IntroText.textInfo;


        while (CurrentLetterIndex < textInfo.characterCount + 1)
        {

            var LastLetter = textInfo.characterCount - 1;

            if (CurrentLetterIndex == LastLetter)
            {
                IntroText.maxVisibleCharacters++;

                yield return FinishDelay;

                TextComplete();

                yield break;

            }


            char character = textInfo.characterInfo[CurrentLetterIndex].character;

           IntroText.maxVisibleCharacters++;

            LetterRevealed();


            if(character == '?' || character == '.' || character == ',' || character == ':' || character == '%' || character == ')' ||
                      character == '!')
            {
                yield return SentenceDelay;
            }
            else
            {
                yield return Delay;
            }

            CurrentLetterIndex++;
        }

    }


    private void TextComplete()
    {
        SceneManager.LoadScene(1);
    }


    private void LetterRevealed()
    {
        TypeNoise.Play();
    }


}
