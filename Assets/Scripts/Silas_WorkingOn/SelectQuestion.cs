﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectQuestion : MonoBehaviour
{
    public GameManager gameManager;
    private int penalty;
    private float counter;
    private float maxCounter = .45f;
    private bool didGetWrong = false;

    //Audios
    public AudioSource answersSounds;
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioSource loseSoundsManager;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Start()
    {
        penalty = 100;
        gameManager = gameObject.GetComponent<GameManager>();
        counter = maxCounter;

        //Audio Initializers
        answersSounds = GetComponent<AudioSource>();

        loseSoundsManager = GetComponent<AudioSource>();

        correctAnswer = GetComponent<AudioClip>();

        wrongAnswer = GetComponent<AudioClip>();
    }

    private void Update()
    {
        if (didGetWrong == true)
        {
            //plays sound if player answered incorectly
            answersSounds.PlayOneShot(wrongAnswer);


            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                didGetWrong = false;
                gameManager.ForceUpdateUIText();
                counter = maxCounter;
                gameManager.moneyEarned -= penalty;
                penalty *= 2;

                if (gameManager.moneyEarned < 0)
                {
                    gameManager.question_Text.text = "Wow, You lost...";
                    gameManager.answerOneGame_Button.SetActive(false);
                    gameManager.answerTwoGame_Button.SetActive(false);
                    gameManager.answerThreeGame_Button.SetActive(false);
                    gameManager.answerFourGame_Button.SetActive(false);

                    loseSoundsManager.PlayOneShot(loseSound);
                }//If player somehow looses their money, they lose... Shocker!
            }
        }// If they pick wrong, they lose some money.
    }

    public void QuestionSelected()
    {
        //Plays sound when player answers correctly
        answersSounds.PlayOneShot(correctAnswer);

        gameManager.SetCurrentQuestion();
    }
            
    /// <summary>
    /// If button one is clicked
    /// </summary>
    public void ansOne()
    {
        if (gameManager.currentAnswerOne.questionValue == gameManager.currentQuestion.isCorrectAnswer)
        {
            gameManager.SetCurrentQuestion();
        }
        else
        {
            didGetWrong = true;
            gameManager.question_Text.text = "Wrong";
            //print("DEBUG: " + gameManager.currentAnswerOne.questionValue + " || " + gameManager.currentQuestion.isCorrectAnswer);
        }
    }
    /// <summary>
    /// If button two is clicked
    /// </summary>
    public void ansTwo()
    {
        if (gameManager.currentAnswerTwo.questionValue == gameManager.currentQuestion.isCorrectAnswer)
        {
            gameManager.SetCurrentQuestion();
        }
        else
        {
            didGetWrong = true;
            gameManager.question_Text.text = "Wrong";
            //print("DEBUG: " + gameManager.currentAnswerTwo.questionValue + " || " + gameManager.currentQuestion.isCorrectAnswer);
        }
    }
    /// <summary>
    /// If button three is clicked
    /// </summary>
    public void ansThree()
    {
        if (gameManager.currentAnswerThree.questionValue == gameManager.currentQuestion.isCorrectAnswer)
        {
            gameManager.SetCurrentQuestion();
        }
        else
        {
            didGetWrong = true;
            gameManager.question_Text.text = "Wrong";
            //print("DEBUG: " + gameManager.currentAnswerThree.questionValue + " || " + gameManager.currentQuestion.isCorrectAnswer);
        }
    }
    /// <summary>
    /// If button four is clicked
    /// </summary>
    public void ansFour()
    {
        if (gameManager.currentAnswerFour.questionValue == gameManager.currentQuestion.isCorrectAnswer)
        {
            gameManager.SetCurrentQuestion();
        }
        else
        {
            didGetWrong = true;
            gameManager.question_Text.text = "Wrong";
            //print("DEBUG: " + gameManager.currentAnswerFour.questionValue + " || " + gameManager.currentQuestion.isCorrectAnswer);
        }
    }
}
