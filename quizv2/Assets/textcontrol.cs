using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class textcontrol : MonoBehaviour
{
    List<string> questions = new List<string>() { "Was ist die Hauptstadt von Deutschland", "Was ist die Hauptstadt von den USA", "Was ist die Hauptstadt von Frankreich", "Was ist die Hauptstadt von Oesterreich", "Was ist die Hauptstadt von Italien", "Wie macht der Hund ", "Wie macht die Katze ", "Wie macht der Frosch ", "Wie macht die Kuh ", "Wie macht der Kuckuk "};

    List<string> correctAnswer = new List<string>() { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };

    List<int> previousQuestions = new List<int>() { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    List<string> unansweredQuestions;
    
    public int questionNumber = 0;

    public Transform resultobj;

    public static string selectedAnswer;

    public static string choiceSelected = "n";

    public static int randQuestion=-1;

    public int catMod = 0;

    public Transform auraobj;

    public double totalCorrect = 0;

    public double totalQuestion = 0;

    public Transform scoreObj;

    public double scorePer;
   

    void Start()
    {
        
        if (Auswahl.catTopic == "Tierlaute")
        {
            catMod = 5;
        }
        if (Auswahl.catTopic == "Hauptstaedte")
        {
            catMod = 0;
        }

    }

    
    void Update(){

        //scoreObj.GetComponent<TextMesh>().text = "Score :  " + totalCorrect;
        if (totalQuestion > 0)
        {
            scorePer = (totalCorrect/totalQuestion)*100;
            scorePer = Math.Round(scorePer);
        }

        scoreObj.GetComponent<TextMesh>().text = "Score :  " + scorePer+"%";
        

        if (randQuestion == -1)
        {
            randQuestion = UnityEngine.Random.Range(0+catMod, 5+catMod);
            resultobj.GetComponent<TextMesh>().text = "";
            for(int i=0; i < 10; i++)
            {
                if(randQuestion != previousQuestions[i])
                {

                }
                else
                {
                    randQuestion = -1;
                }
            }
        }
        if (randQuestion > -1)
        {
            GetComponent<TextMesh>().text = questions[randQuestion];
        }
        if (choiceSelected=="y")
        {
            choiceSelected = "n";
            totalQuestion += 1;
            previousQuestions[questionNumber] = randQuestion;
            questionNumber += 1;
            if (correctAnswer[randQuestion] == selectedAnswer)
                totalCorrect += 1;
            {
                resultobj.GetComponent<TextMesh>().color = new Color(0, 1, 0);
                resultobj.GetComponent<TextMesh>().text = "Correct Click next to continue";
               
            }
            if (correctAnswer[randQuestion] != selectedAnswer)
            {
                NextButton.resetAura = "n";
                resultobj.GetComponent<TextMesh>().color = new Color(1, 0, 0);
                resultobj.GetComponent<TextMesh>().text = "Incorrect Click next to continue";
              
            }
        }
    }
}
