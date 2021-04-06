using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

public class MatchMaker : NetworkBehaviour {

    public static MatchMaker instance;          // Singleton, so that there's always just one MatchMaker in the currently running Scene

    public SyncListMatch matches = new SyncListMatch();
    public SyncList<String> matchIDs = new SyncList<String>();

    [SerializeField] GameObject turnManagerPrefab;

    void Start() {
        instance = this;
    }

    // ***** Host-Section *****

    // Checks whether randomly created matchID already exists, quick validation method
    public bool HostMatch(string _matchID, GameObject _player) { // add "out int playerIndex" to parameters if you want to add the playerIndex-feature
        // playerIndex = -1;
        if (!matchIDs.Contains(_matchID)) {
            matchIDs.Add(_matchID);                     // adds current Match's ID to the SyncList of matchIDs, so that we can monitor which matchIDs are currently being used
            matches.Add(new Match(_matchID, _player));  // adds current Match to the SyncList of matches, so that we can monitor which matches are currently up
            Debug.Log("Match generated"); 
            // playerIndex = 1;
            return true;
        } else {
            Debug.Log("Match ID already exists");
            return false;
        }
    }

    // ***** Join-Section *****

    // Checks whether matchID exists in the matchIDs-SyncList. If so, the player is added to the respective match
    // If matchID doesn't exist, joining a match fails
    public bool JoinMatch(string _matchID, GameObject _player) {  // add "out int playerIndex" to parameters if you want to add the playerIndex-feature
        if (matchIDs.Contains(_matchID)) {
            for (int i = 0; i<matches.Count; i++) {
                if (matches[i].matchID == _matchID) {
                    matches[i].players.Add(_player);
                    // playerIndex = matches[i].players.Count -1;
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

    // Starts a TurnManager
    // Searches for correct match (this player is in), gets all the players in the match and tells them to start their game
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

    // Generates Random Match ID
    // Return: RandomMatchID id (a random identification "string" with upper letters and numbers to identify a match)
    public static string GetRandomMatchID() {           //static so that we can call the function from everywehre
        string id = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26) {
                id += (char)(random + 65);              //<26 then its a letter, 65 is in ASCII the A, so that range starts at capital A instead of lower a
            } else {
                id += (random - 26).ToString();
            }
        }
        Debug.Log($"Random Match ID: {id}");
        return id;
    }

}







[System.Serializable] // when it should be network-compatible
public class Match { // defines what a Match is and what attributes it needs
    public string matchID;
    public SyncListGameObject players = new SyncListGameObject(); // creates a SyncList of all players

    public Match(string _matchID, GameObject _player) {     // _player is the local player
        matchID = _matchID;
        players.Add(_player);
    }

    public Match() { }  //Serializable classes need a standard constructor
}

[System.Serializable]
// creates a new Class which inherits from SyncList, so that we can have a SyncList of GameObjects
public class SyncListGameObject : SyncList<GameObject> {

}

// creates a new Class which inherits from SyncList, so that we can have a SyncList of Matches
public class SyncListMatch : SyncList<Match> {

}

// Class for special methods regarding MatchMaking. These methods extend the existing pool of MatchMaking-classes
// Current classes: ToGuid
public static class MatchExtensions {

    // converts a 5 digit string (our randomly generated matchID) into a Guid (Mirror's special ID for all the GameObjects in a specific match)
    public static Guid ToGuid(this string _id) {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider(); // needed to convert something into a Guid
        byte[] inputBytes = Encoding.Default.GetBytes(_id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
