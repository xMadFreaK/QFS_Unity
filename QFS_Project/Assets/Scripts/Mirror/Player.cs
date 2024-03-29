﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

// Methods to communicate with the Server/MatchMaker.cs. Main tasks: Handling Host, Join, Start + other player's lobbies
public class Player : NetworkBehaviour { //so that this script is networkable

    public static Player localPlayer;       // Singleton to assign the local player to

    [SyncVar] public string matchID;        // Random matchID to identificate a match. It is saved in a SyncList
    // [SyncVar] public int playerIndex        // Index of player in relation to all the other players in the game - not implemented yet

    public string onlineName;
    public int score = 0;

    NetworkMatchChecker networkMatchChecker; // NetworkMatchChecker is the Mirror MatchMaking component. It is used in this script to create the Guid from our matchID

    // Instantiates this player as local player (if that's the case)
    // If this player is not the local player, they are spawned to the lobby as "other player"
    void Start() {
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

        if (isLocalPlayer) {
            localPlayer = this;             //so the system knows the player is the local Player
            onlineName = PlayerInformation.playerName;
        } else {
            onlineName = "Gegner";
            UILobby.instance.SpawnPlayerUIPrefab(this); // spawns this player - which is not the local player - to the lobby
        }    
    }

    // ***** Host-Section *****

    // called by UILobby.cs by pressing on button "Match erstellen"
    public void HostGame() {
        string matchID = MatchMaker.GetRandomMatchID();                  // gets random match ID from MatchMaker.cs
        int seed = Savestuff.CreateSeed(matchID);                        // for asnychrone solution
        Savestuff.seed = seed;                                           //save the generated seed in helper class SaveStuff
        Debug.Log(Savestuff.seed);
        CmdHostGame(matchID);
    }

    [Command]   //to run this CmdHostGame on a Server (= "command" to server/direct communication with the server) - a command method always needs to be prefixed by Cmd...!!!
    // Asks server to host the game
    // Logs whether successful or not
    // called by Player.HostGame with randomly created matchID as parameter
    void CmdHostGame(string _matchID)  {    
        matchID = _matchID;
        if (MatchMaker.instance.HostMatch(_matchID, gameObject))        // add "out playerIndex" to parameters to implement playerIndex
        {                                                               // gameObject from this player (= the GameObject this component is attached to)      
            Debug.Log("Game hosted successfully (Player.CmdHostGame)");
            networkMatchChecker.matchId = _matchID.ToGuid();            // sets matchID of NetworkMatchChecker to our generated _matchID, which needs to be turned into a Guid first, using our ToGuid()-Method
            TargetHostGame(true, _matchID); 
        } else {
            Debug.Log("Game hosted failed (Player.CmdHostGame)");
            TargetHostGame(false, _matchID);
        }   
    }

    [TargetRpc] // target client
    // Transmits to client (UILobby.cs) whether hosting the game was successful or not
    void TargetHostGame(bool _success, string _matchID) {
        Debug.Log("Start Player.TargetHostGame");
        matchID = _matchID;
        Debug.Log("Set MatchID successful Player.TargetHostGame");
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(_success, _matchID);
        Debug.Log("Called HostSuccess in Player.TargetHostGame");
    }


    // ***** Join-Section *****

    public void JoinGame(string _inputID) {
        int seed = Savestuff.CreateSeed(_inputID);
        Savestuff.seed = seed;
        Debug.Log(Savestuff.seed);
        CmdJoinGame(_inputID);
    }

    [Command]
    // Command to server to join game. 
    // Calls TargetJoinGame either with "true" for success or "false" for fail
    void CmdJoinGame(string _matchID) {
        matchID = _matchID;
        if (MatchMaker.instance.JoinMatch(_matchID, gameObject))        // add "out playerIndex" to parameters to implement playerIndex
        {                                                               // gameObject from this player (= the GameObject this component is attached to)
            Debug.Log("Game joined successfully");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID);
        } else {
            Debug.Log("Game joined failed");
            TargetJoinGame(false, _matchID);
        }
    }

    [TargetRpc] //is an information from the server instance directly to the localPlayer
    void TargetJoinGame(bool _success, string _matchID) {
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(_success, _matchID);
    }


    // ***** Begin Match Section *****

    public void BeginGame() {
        CmdBeginGame();
    }

    [Command]
    // tells server to begin game
    void CmdBeginGame() {
        MatchMaker.instance.BeginMatch(matchID);
        Debug.Log("Game Beginning");
        }

    public void StartGame() {
        TargetBeginGame();
    }

    [TargetRpc] //is an information from the server instance directly to the localPlayer
    public void TargetBeginGame() {
        Debug.Log($"MatchID: {matchID} beginning");
        //Additively load game scene
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    //[ClientRpc] this method is created for the ServerLogic.cs script
    //public void RpcQuesionsHandOverToClients(Question _question) {
        //GameManager.UpdateAnswers(_question);   error in function, doesn´t work with this GameManager                                           
    //}
}

    


