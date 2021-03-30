using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour { //so that this script is networkable

    public static Player localPlayer;       //Singleton

    [SyncVar] public string matchID;

    NetworkMatchChecker networkMatchChecker;

    void Start() {
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

        if (isLocalPlayer) {
            localPlayer = this;             //so the system knows the player is the local Player
        } else {
            UILobby.instance.SpawnPlayerUIPrefab(this);
        }    
    }

    //Host-Section
    public void HostGame() {
        string matchID = MatchMaker.GetRandomMatchID();     //1st we want to get a Random Match-ID
        CmdHostGame(matchID);
    }

    [Command]   //to run this CmdHostGame on a Server
    void CmdHostGame(string _matchID) {
        matchID = _matchID;
        if (MatchMaker.instance.HostMatch(_matchID, gameObject)) {      //gameObject from this player
            Debug.Log("Game hosted successfully");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetHostGame(true, _matchID);
        } else {
            Debug.Log("Game hosted failed");
            TargetHostGame(false, _matchID);
        }   
    }

    [TargetRpc]
    void TargetHostGame(bool _success, string _matchID) {
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(_success, _matchID);
    }


    //Join-Section

    public void JoinGame(string _inputID) {
        CmdJoinGame(_inputID);
    }

    [Command]   //to run this CmdHostGame on a Server
    void CmdJoinGame(string _matchID) {
        matchID = _matchID;
        if (MatchMaker.instance.JoinMatch(_matchID, gameObject)) {      //gameObject from this player
            Debug.Log("Game joined successfully");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID);
        } else {
            Debug.Log("Game joined failed");
            TargetJoinGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool _success, string _matchID) {
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(_success, _matchID);
    }


    //Begin Match Section
    public void BeginGame() {
        CmdBeginGame();
    }

    [Command]   //to run this CmdHostGame on a Server
    void CmdBeginGame() {
        MatchMaker.instance.BeginMatch(matchID);
        Debug.Log("Game Beginning");
        }

    public void StartGame() {
        TargetBeginGame();
    }

    [TargetRpc]
    public void TargetBeginGame() {
        Debug.Log($"MatchID: {matchID} beginning");
        //Additively load game scene
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}

    


