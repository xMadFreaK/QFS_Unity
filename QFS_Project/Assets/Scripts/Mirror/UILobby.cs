using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class mainly takes care of user inputs and outputs during Server-Connection process
public class UILobby : MonoBehaviour {

    public static UILobby instance;             // Singleton - instance of a client

    [Header("Host Join")]

    [SerializeField] TMP_InputField matchIDInput;
    [SerializeField] Button joinButton;                 //need reference to join and host button to be able to make these interactable
    [SerializeField] Button hostButton;

    [SerializeField] Canvas lobbyCanvas;

    [Header("Lobby")]

    [SerializeField] Transform UIPlayerParent;
    [SerializeField] GameObject UIPlayerPrefab;
    [SerializeField] TextMeshProUGUI matchIDText;
    [SerializeField] GameObject beginGameButton;

    void Start() {
        instance = this;      
    }

    // When pressing "Match erstellen", buttons will be deactivated to prevent spamming.
    // The local player is now hosting the game.
    public void Host() {
        matchIDInput.interactable = false;      //to not be able to spam the buttons. they are reactivated upon game start (UILobby.BeginGame)
        joinButton.interactable = false;
        hostButton.interactable = false;

        Player.localPlayer.HostGame();          // localPlayer will host the game
    }

    // If Hosting was not successful, the Host/Join/MatchID-Input-Buttons become interactable again
    // If Hosting was successful, the Lobby is enabled, the PlayerUIPrefab is spawned and the BeginGameButton is set active
    public void HostSuccess(bool _success, string _matchID) {
        if (_success) {
            Debug.Log("UILobby.HostSuccess works");
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = _matchID;
            beginGameButton.SetActive(true);
        } else {
            Debug.Log("UILobby.HostSuccess failed");
            matchIDInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }

    }

    // When pressing Beitreten-Button, buttons will be deactivated to prevent spamming.
    // The local player is now joining the game
    public void Join() {
        matchIDInput.interactable = false;      //to not be able to spam the buttons. they are reactivated upon game start (UILobby.BeginGame)
        joinButton.interactable = false;
        hostButton.interactable = false;

        Player.localPlayer.JoinGame(matchIDInput.text.ToUpper());
    }

    // If joining was successful, the Lobby is enabled and the player spawns, the matchID is shown
    // If joining was unsuccessful, the Host/Join/machtIDInput-Buttons are enabled again
    public void JoinSuccess(bool _success, string _matchID) {
        if (_success) {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = _matchID;
        } else {
            matchIDInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }
    }

    // Spawns player into lobby
    public void SpawnPlayerUIPrefab(Player _player) {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer>().SetPlayer(_player);
    }

    // starts game for local player
    public void BeginGame() {
        Player.localPlayer.BeginGame();
        matchIDInput.interactable = true;      //to reactivate the buttons in case you'd like to play again
        joinButton.interactable = true;
        hostButton.interactable = true;
    }

}
