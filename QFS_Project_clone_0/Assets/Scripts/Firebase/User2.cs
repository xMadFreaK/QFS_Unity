
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User2 {
    public string localId;
    public string userName;
    public int zgames;
    public int zwins;
    public int zlosses;
    public int yquantLoggedIn;
    public int yClicks;
    
   

    public User2() {
        localId = PlayerInformation.localId;
        userName = PlayerInformation.playerName;
        zgames = PlayerInformation.games;
        zwins = PlayerInformation.wins;
        zlosses = PlayerInformation.losses;
        yquantLoggedIn = PlayerInformation.quantityLoggedIn;
        yClicks = PlayerInformation.clicks;
    }
}