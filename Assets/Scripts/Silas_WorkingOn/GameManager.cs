using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //------------ User Interface ------------
    //---------- Title Screen UI ----------
    //----- Images -----
    public Image backgroundImageTitle_UI;
    //----- Buttons -----
    public GameObject quitButton_Button;
    public GameObject restartButton_Button;
    //---------- In Game UI ----------
    //----- Images -----
    public Image backgroundImageGame_UI;
    //----- Texts -----
    //-- Timers --
    public Text totalMoney_Text;
    public Text questionTimer_Text;
    //-- Question/Answers --
    [SerializeField]
    public Text question_Text;
    public Text questionNumber_Text;
    public Text answerOne_Text;
    public Text answerTwo_Text;
    public Text answerThree_Text;
    public Text answerFour_Text;
    //----- Buttons -----
    public GameObject answerOneGame_Button;
    public GameObject answerTwoGame_Button;
    public GameObject answerThreeGame_Button;
    public GameObject answerFourGame_Button;
    public GameObject fiftyFifty_Button;
    public GameObject askTheAudience_Button;
    public GameObject callAFriend_Button;
    //----- Strings -----
    public Question[] questions;
    public AnswersOne[] answersOne;
    public AnswersTwo[] answersTwo;
    public AnswersThree[] answersThree;
    public AnswersFour[] answersFour;
    public QuestionWorth[] moneyToEarn;
    //----- Lists -----
    private static List<Question> unansweredQuestions;
    private static List<AnswersOne> unselectedAnswersOne;
    private static List<AnswersTwo> unselectedAnswersTwo;
    private static List<AnswersThree> unselectedAnswersThree;
    private static List<AnswersFour> unselectedAnswersFour;
    private static List<QuestionWorth> unearnedMoney;
    // ----- Current Question and Answers -----
    public AnswersOne currentAnswerOne;
    public AnswersTwo currentAnswerTwo;
    public AnswersThree currentAnswerThree;
    public AnswersFour currentAnswerFour;
    public QuestionWorth currentEarnableMoney;

    //------------ Variables ------------
    //----- Timers -----
    public float questionTimer;
    //----- Points -----
    public int moneyEarned;
    public int moneyGained;
    //----- Questions -----
    private int totalQuestions;
    public int questionBeingAnswered;
    public Question currentQuestion;

    // Use this for initialization
    void Start()
    {
        restartButton_Button.SetActive(false);
        quitButton_Button.SetActive(false);

        totalQuestions = questions.Count<Question>();
        //If question/answers is null or 0, sets them their correct value
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        if (unselectedAnswersOne == null || unselectedAnswersOne.Count == 0)
        {
            unselectedAnswersOne = answersOne.ToList<AnswersOne>();
        }
        if (unselectedAnswersTwo == null || unselectedAnswersTwo.Count == 0)
        {
            unselectedAnswersTwo = answersTwo.ToList<AnswersTwo>();
        }
        if (unselectedAnswersThree == null || unselectedAnswersThree.Count == 0)
        {
            unselectedAnswersThree = answersThree.ToList<AnswersThree>();
        }
        if (unselectedAnswersFour == null || unselectedAnswersFour.Count == 0)
        {
            unselectedAnswersFour = answersFour.ToList<AnswersFour>();
        }
        if (unearnedMoney == null || unearnedMoney.Count == 0)
        {
            unearnedMoney = moneyToEarn.ToList<QuestionWorth>();
        }
        SetCurrentQuestion();
        
    }
    /// <summary>
    /// Selects a question randomly and updates UI + answers
    /// </summary>
    public void SetCurrentQuestion()
    {
        if (unansweredQuestions.Count > 0)
        {
            int randomQuestionSelector = Random.Range(0, unansweredQuestions.Count); // Selects a question at random

            //Sets the current question the player is on.
            questionBeingAnswered += 1;
            questionNumber_Text.text = ("Question: " + questionBeingAnswered + " / " + totalQuestions);

            //Sets the current question, answers, and reward Money to the randomly selected remaining questions and corrisponding values
            currentQuestion = unansweredQuestions[randomQuestionSelector];
            currentAnswerOne = unselectedAnswersOne[randomQuestionSelector];
            currentAnswerTwo = unselectedAnswersTwo[randomQuestionSelector];
            currentAnswerThree = unselectedAnswersThree[randomQuestionSelector];
            currentAnswerFour = unselectedAnswersFour[randomQuestionSelector];
            currentEarnableMoney = unearnedMoney[0];

            //Updates Text UI to match the question
            question_Text.text = currentQuestion.question;
            answerOne_Text.text = currentAnswerOne.answers;
            answerTwo_Text.text = currentAnswerTwo.answers;
            answerThree_Text.text = currentAnswerThree.answers;
            answerFour_Text.text = currentAnswerFour.answers;

            // Removes question that was just asked, and the answers connected to it.
            unansweredQuestions.RemoveAt(randomQuestionSelector);
            unselectedAnswersOne.RemoveAt(randomQuestionSelector);
            unselectedAnswersTwo.RemoveAt(randomQuestionSelector);
            unselectedAnswersThree.RemoveAt(randomQuestionSelector);
            unselectedAnswersFour.RemoveAt(randomQuestionSelector);

        }
        else
        {
            answerOneGame_Button.SetActive(false);
            answerTwoGame_Button.SetActive(false);
            answerThreeGame_Button.SetActive(false);
            answerFourGame_Button.SetActive(false);
            fiftyFifty_Button.SetActive(false);
            askTheAudience_Button.SetActive(false);
            callAFriend_Button.SetActive(false);
            question_Text.text = "You Won!\n Your prize is $" + moneyEarned;
        }

        //Sets the value of the question to match the questions answered.
        int x = questionBeingAnswered - 1;//doing this made it work, Im not going to question it.
        moneyEarned += moneyGained;
        currentEarnableMoney = unearnedMoney[x];
        moneyGained = currentEarnableMoney.worth;
        totalMoney_Text.text = "Money: $" + moneyEarned;
        ForceUpdateUIText();
    }
    // Updates timer.
    public void Update()
    {
        questionTimer += Time.deltaTime;
        questionTimer_Text.text = questionTimer.ToString("00:00");
    }
    /// <summary>
    /// Force updates text UI and buttons
    /// </summary>
    public void ForceUpdateUIText()
    {
        answerOneGame_Button.SetActive(true);
        answerTwoGame_Button.SetActive(true);
        answerThreeGame_Button.SetActive(true);
        answerFourGame_Button.SetActive(true);

        question_Text.text = currentQuestion.question;
        answerOne_Text.text = currentAnswerOne.answers;
        answerTwo_Text.text = currentAnswerTwo.answers;
        answerThree_Text.text = currentAnswerThree.answers;
        answerFour_Text.text = currentAnswerFour.answers;
        totalMoney_Text.text = "Money: $" + moneyEarned;
    }
    /// <summary>
    /// Handles all funtions for the 50/50 LifeLine
    /// </summary>
    public void FiftyFifty()
    {
        int ranOne = Random.Range(1, 3);
        int ranTwo = Random.Range(1, 3);

        while (ranOne == ranTwo)
        {
            ranTwo = Random.Range(1, 3);
        } //if ranOne = ranTwo re randomise ranTwo

        if (currentAnswerOne.questionValue == currentQuestion.isCorrectAnswer)
        {
            if (ranOne == 1)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranOne == 2)
            {
                answerThreeGame_Button.SetActive(false);
            }
            else if (ranOne == 3)
            {
                answerFourGame_Button.SetActive(false);
            }

            if (ranTwo == 1)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranTwo == 2)
            {
                answerThreeGame_Button.SetActive(false);
            }
            else if (ranTwo == 3)
            {
                answerFourGame_Button.SetActive(false);
            }
        }//If question One is correct, select from the remaining 3

        if (currentAnswerTwo.questionValue == currentQuestion.isCorrectAnswer)
        {
            if (ranOne == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranOne == 2)
            {
                answerThreeGame_Button.SetActive(false);
            }
            else if (ranOne == 3)
            {
                answerFourGame_Button.SetActive(false);
            }

            if (ranTwo == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranTwo == 2)
            {
                answerThreeGame_Button.SetActive(false);
            }
            else if (ranTwo == 3)
            {
                answerFourGame_Button.SetActive(false);
            }
        }//If question Two is correct, select from the remaining 3

        if (currentAnswerThree.questionValue == currentQuestion.isCorrectAnswer)
        {
            if (ranOne == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranOne == 2)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranOne == 3)
            {
                answerFourGame_Button.SetActive(false);
            }

            if (ranTwo == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranTwo == 2)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranTwo == 3)
            {
                answerFourGame_Button.SetActive(false);
            }
        }//If question Three is correct, select from the remaining 3

        if (currentAnswerFour.questionValue == currentQuestion.isCorrectAnswer)
        {
            if (ranOne == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranOne == 2)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranOne == 3)
            {
                answerThreeGame_Button.SetActive(false);
            }

            if (ranTwo == 1)
            {
                answerOneGame_Button.SetActive(false);
            }
            else if (ranTwo == 2)
            {
                answerTwoGame_Button.SetActive(false);
            }
            else if (ranTwo == 3)
            {
                answerThreeGame_Button.SetActive(false);
            }
        }//If question Four is correct, select from the remaining 3
        fiftyFifty_Button.SetActive(false);
    }
    /// <summary>
    /// Handles all funtions for the Ask the Audience LifeLine
    /// </summary>
    public void AskTheAudiance()
    {
        int A = Random.Range(0, 100);
        int B = Random.Range(0, 100);
        int C = Random.Range(0, 100);
        int D = Random.Range(0, 100);
        //The audience are robots, what did you expect? 
        question_Text.text = "The audience says... A:" + A + ", B: " + B + ", C: " + C + ", D: " + D;
        askTheAudience_Button.SetActive(false);
    }
    /// <summary>
    /// Handles all funtions for the Ask the
    /// </summary>
    public void CallAFriend()
    {
        question_Text.text = "What do you want? Im bus- Oh? You're on a game show? And called me... Well, I think it is *clears throat* %&@$-- *Phone beep*";
        callAFriend_Button.SetActive(false);
    }//Wow, your friend seems really... Nice.
}
