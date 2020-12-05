using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerQFS networkManager = null; // reference to NetworkManager

    [Header("UI")] // reference to landing page panel
    [SerializeField] private GameObject landingPagePanel = null;

    // method is called when one player is the host
    public void HostLobby() {
        networkManager.StartHost(); // start network manager as host
        landingPagePanel.SetActive(false); // disables landing page panel
    }
}
