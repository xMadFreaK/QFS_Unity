using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class User
{
    public string userName;
    public int userScore;

    public User() {
        userName = FBPlayerScores.playerName;
        userScore = FBPlayerScores.playerScore;
    }
}
