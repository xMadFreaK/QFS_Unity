using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

public class MatchMaker : NetworkBehaviour {

    public static MatchMaker instance;          //Singleton

    public SyncListMatch matches = new SyncListMatch();
    public SyncList<String> matchIDs = new SyncList<String>();

    [SerializeField] GameObject turnManagerPrefab;

    void Start() {
        instance = this;
    }

    //Host-Section
    public bool HostMatch(string _matchID, GameObject _player) {
        if (!matchIDs.Contains(_matchID)) {
            matchIDs.Add(_matchID);
            matches.Add(new Match(_matchID, _player));
            Debug.Log("Match generated");
            return true;
        } else {
            Debug.Log("Match ID already exists");
            return false;
        }
    }
    //Join-Section
    public bool JoinMatch(string _matchID, GameObject _player) {
        if (matchIDs.Contains(_matchID)) {
            for (int i = 0; i<matches.Count; i++) {
                if (matches[i].matchID == _matchID) {
                    matches[i].players.Add(_player);
                    break;
                }
            }
            Debug.Log("Match joined");
            return true;
        } else {
            Debug.Log("Match ID does not exist");
            return false;
        }
    }

    public void BeginMatch(string _matchID) {
        GameObject newTurnManager = Instantiate(turnManagerPrefab);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatchChecker>().matchId = _matchID.ToGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++) {
            if (matches[i].matchID == _matchID) {
                foreach (var player in matches[i].players) {
                    Player _player = player.GetComponent<Player>();
                    turnManager.AddPlayer(_player);
                    _player.StartGame();
                }
                break;
            }

        }
    }
    public static string GetRandomMatchID() {           //static so that we can call the function from everywehre
        string id = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26) {
                id += (char)(random + 65);              //<26 then its a letter, 65 is in ASCII the A
            } else {
                id += (random - 26).ToString();
            }
        }
        Debug.Log($"Random Match ID: {id}");
        return id;
    }

}















[System.Serializable] //when it should be network-compatible
public class Match {
    public string matchID;
    public SyncListGameObject players = new SyncListGameObject();

    public Match(string _matchID, GameObject _player) {     // _player is the host
        matchID = _matchID;
        players.Add(_player);
    }

    public Match() { }  //Serializable classes need a standard constructor
}





[System.Serializable]
public class SyncListGameObject : SyncList<GameObject> {

}

public class SyncListMatch : SyncList<Match> {

}

public static class MatchExtensions {

    //dont think too much about this method, converts a 5 digit string into a Guid
    public static Guid ToGuid(this string _id) {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(_id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
