using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILobby : MonoBehaviour {

    public static UILobby instance;

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

    //when pressing HOST-Button
    public void Host() {
        matchIDInput.interactable = false;      //to not be able to spam the buttons
        joinButton.interactable = false;
        hostButton.interactable = false;

        Player.localPlayer.HostGame();          //we as the localPlayer will host the game
    }

    public void HostSuccess(bool _success, string _matchID) {    //when hosting was not successfull, we can press the buttons again
        if (_success) {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = _matchID;
            beginGameButton.SetActive(true);
        } else {
            matchIDInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }

    }

    //when pressing Beitreten-Button
    public void Join() {
        matchIDInput.interactable = false;      //to not be able to spam the buttons
        joinButton.interactable = false;
        hostButton.interactable = false;

        Player.localPlayer.JoinGame(matchIDInput.text.ToUpper());
    }

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

    public void SpawnPlayerUIPrefab(Player _player) {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer>().SetPlayer(_player);
    }

    public void BeginGame() {
        Player.localPlayer.BeginGame();
    }

}
