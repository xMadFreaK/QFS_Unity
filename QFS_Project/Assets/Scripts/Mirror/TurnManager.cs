using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : NetworkBehaviour {

    List<Player> players = new List<Player>();

    //add new player and initialize the current score to 0
    public void AddPlayer(Player _player) {
        _player.score = 0;
        players.Add(_player);
    }



}
