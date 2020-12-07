using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerQFS networkManager = null; // reference to network manager

    [Header("UI")] // reference to landing page
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null; // reference to input field with ip address
    [SerializeField] private Button joinButton = null; // reference to join button

    private void OnEnable() {
        NetworkManagerQFS.OnClientConnected += HandleClientConnected;
        NetworkManagerQFS.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable() {
        NetworkManagerQFS.OnClientConnected -= HandleClientConnected;
        NetworkManagerQFS.OnClientDisconnected -= HandleClientDisconnected;
    }

    // called upon click on "join" button
    public void JoinLobby() {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress; // set network address (from network manager) to be this ip address (eg localhost)
        networkManager.StartClient(); // calls StartClient and uses above network address

        joinButton.interactable = false; // disable join button
    }

    private void HandleClientConnected() {
        joinButton.interactable = true; // join button is enabled again in case they get thrown out and need to join again
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    // is called upon disconnecting OR also when failing to connect
    private void HandleClientDisconnected() {
        joinButton.interactable = true; // enable join button again
    }
}
