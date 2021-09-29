//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;
//using FullSerializer;
//using Proyecto26;

//public class ServerLogic : NetworkBehaviour {
//    //I have implemented this class regarding my possible proceeding description. Unfortunately it came out the GameManager script and the 
//    // whole Game scene need to be adjusted to have a possible Server Game flow like we wanted. (Michael Veitz)

//    //variables to save the players in this class
//    public Player localPlayer;
//    public Player notLocalPlayer;

//    Question[] questions = new Question[5];   //our quiz has got 5 questions each round      
//    public int questionIndex = 0;

//    public int scoreLocalPlayer = 0;
//    public int scoreNotLocalPlayer = 0;

//    //constructor
//    public ServerLogic(Player localPlayer, Player notLocalPlayer) {
//        this.localPlayer = localPlayer;
//        this.notLocalPlayer = notLocalPlayer;
//    }

//    //filling our questions-array
//    public void FillQuestions(Question _question) {
//        questions[questionIndex] = _question;
//        questionIndex++;                                //by the next call of this function the next index will be filled
//    }

//    //increase score
//    public void IncreaseScoreLocalPlayer(int addition) {
//        scoreLocalPlayer = scoreLocalPlayer + addition;
//    }

//    //give each question to both clients
//    public void HandOverQuestionsToClients() {
//        for (int i = 0; i < questions.Length; i++) {
//            //localPlayer.RpcQuesionsHandOverToClients(questions[i]);
//        }
//    }

//    //players commit their score after the answers to the server-script
//    [Command]
//    void CmdCommitScoreAfterAnswers(int _addition) {                //unfortunately there is no reference in the code, I figured out, it´s not working this way,
//                                                                    //this method should be called normally by Player.cs skript

//    }

//    //transmit result of the game
//    [Command]
//    void CmdCommitWinner(Player _localPlayer, Player _notLocalPlayer) {
//        if (_localPlayer.score > _notLocalPlayer.score) {
//            //IncreaseWins();                                       //methods not working in this ServerLogic script due to reference
//        } else if (_localPlayer.score < _notLocalPlayer.score) {
//            //IncreaseLosses();
//        } else {
//            //Draw()
//        }
//    }


//}
