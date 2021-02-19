
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User1 {
    public string userName;
    public int userScore;
    public string localId;
    public int matches;

    public User1() {
        userName = PlayerScores.playerName;
        userScore = PlayerScores.playerScore;
        localId = PlayerScores.localId;
        matches = PlayerScores.matches;
    }
}