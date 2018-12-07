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
    public Image imageTitle_UI;
    public Image backgroundImageTitle_UI;
    //----- Buttons -----
    public GameObject startButtonTitle_Button;
    public GameObject quitButtonTitle_Button;
    //---------- In Game UI ----------
    //----- Images -----
    public Image backgroundImageGame_UI;
    //----- Texts -----
    //-- Timers --
    public Text totalTimer_Text;
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
    //----- Strings -----
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    public AnswersOne[] answersOne;
    private static List<AnswersOne> unselectedAnswersOne;
    public AnswersOne currentAnswerOne;

    public AnswersTwo[] answersTwo;
    private static List<AnswersTwo> unselectedAnswersTwo;
    public AnswersTwo currentAnswerTwo;

    public AnswersThree[] answersThree;
    private static List<AnswersThree> unselectedAnswersThree;
    public AnswersThree currentAnswerThree;

    public AnswersFour[] answersFour;
    private static List<AnswersFour> unselectedAnswersFour;
    public AnswersFour currentAnswerFour;

    //------------ Variables ------------
    //----- Timers -----
    private float questionTimer;
    //----- Points -----
    private int pointsTotal;
    public int pointsEarned;
    //----- Questions -----
    private int totalQuestions;
    public int questionBeingAnswered;
    public Question currentQuestion;


    //--------- SerializedField ---------
    //[SerializeField]
    //private Text questionText_Text;


    // Use this for initialization
    void Start()
    {
        totalQuestions = questions.Count<Question>();
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

        SetCurrentQuestion();
        
    }
    public void SetCurrentQuestion() // Selects a question randomly
    {
        if (unansweredQuestions.Count > 0)
        {
            int randomQuestionSelector = Random.Range(0, unansweredQuestions.Count); // Selects a question at random
            currentQuestion = unansweredQuestions[randomQuestionSelector]; // Sets current question to randomly selected one

            question_Text.text = currentQuestion.question;
            questionBeingAnswered += 1;

            unansweredQuestions.RemoveAt(randomQuestionSelector); // Removes question that was just asked.
            questionNumber_Text.text = ("Question: " + questionBeingAnswered + " / " + totalQuestions);


            //
            currentAnswerOne = unselectedAnswersOne[randomQuestionSelector];
            currentAnswerTwo = unselectedAnswersTwo[randomQuestionSelector];
            currentAnswerThree = unselectedAnswersThree[randomQuestionSelector];
            currentAnswerFour = unselectedAnswersFour[randomQuestionSelector];

            answerOne_Text.text = currentAnswerOne.answers;
            answerTwo_Text.text = currentAnswerTwo.answers;
            answerThree_Text.text = currentAnswerThree.answers;
            answerFour_Text.text = currentAnswerFour.answers;
        }
        else
            print("DEBUG: Player Won");
    }
}
