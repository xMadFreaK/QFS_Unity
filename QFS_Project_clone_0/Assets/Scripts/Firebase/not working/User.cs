using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class User
{
    public string userName;
    public int userScore;
    public string localID;

    public User() {
        userName = FBPlayerScores.playerName;
        userScore = FBPlayerScores.playerScore;
        localID = FBPlayerScores.localId;
    }
}
