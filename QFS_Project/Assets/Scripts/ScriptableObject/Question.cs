﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

public enum AnswerType { Multi, Single }

/*[Serializable()]
public class Answer
{
    public string       Info         = string.Empty;
    public bool         IsCorrect    = false;

    public Answer() { }
}*/
[Serializable()]
public class Question {

    public String       Info         = null;
    //public Answer[] Answers = null;
    public string[] Answer = new string[4];
    public Boolean      UseTimer     = false;
    public Int32        Timer        = 0;
    public AnswerType   Type         = AnswerType.Single;
    public Int32        AddScore     = 0;

    public Question () { }

    /// <summary>
    /// Function that is called to collect and return correct answers indexes.
    /// </summary>
    /* public List<int> GetCorrectAnswers ()
     {
         List<int> CorrectAnswers = new List<int>();
         for (int i = 0; i < Answer.Length; i++)
         {
             if (Answer[i].IsCorrect)
             {
                 CorrectAnswers.Add(i);
             }
         }
         return CorrectAnswers;
     }
    }*/

    /*public struct Question
    {

        public String Info;
        //public Answer[] Answers = null;
        public string[] Answer ;
        public Boolean UseTimer ;
        public Int32 Timer ;
        public AnswerType Type ;
        public Int32 AddScore ;

        //public Question() { }
    */

        public List<int> GetCorrectAnswers()
    {
        List<int> CorrectAnswers = new List<int>();
         CorrectAnswers.Add(0);
        return CorrectAnswers;
    }
}

public class Quizz
{
    public static Question[] getQuiz(string datei)
    {
        List<Question> liste = new List<Question>();
        string[] zeilen = File.ReadAllLines(datei);

        foreach(string zeile in zeilen)
        {
            string[] data = zeile.Split(';');
            Question q = new Question();
            q.Info = data[0];
            int e = 1;
            for(int i = 0; i <= 3; i++){
                q.Answer[i] = data[e];
                e++;
            }
            int x;
            int.TryParse(data[5], out x);
            if(x > 0)
            {
                q.UseTimer = true;
                q.Timer = x;
            }else
            {
                q.UseTimer = false;
            }
            int.TryParse(data[6], out q.AddScore);

            liste.Add(q);
        }
        return liste.ToArray();
    }

    public static void Ausprobieren()
    {
        var quizz = Quizz.getQuiz(@"C:\Users\Linda Huber\Downloads\Q1neu.csv");

        int index = 0;
        int anzahl = quizz.Length;

        while (index < anzahl)
        {
            print(quizz[index].Info);
            print(quizz[index].Answer[0]);
            print(quizz[index].Timer);
            print(quizz[index].AddScore);
           
            index++;
        }
    }
}

