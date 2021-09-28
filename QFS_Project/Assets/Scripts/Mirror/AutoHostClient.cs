using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoHostClient : MonoBehaviour {

    [SerializeField] NetworkManager networkManager;

    // checks and logs whether application is Server or Client.
    // If application is client, the NetworkManager starts the client immediately instead of asking the user whether they are host or client
    void Start() {
        if (!Application.isBatchMode) { // if this is not the server (isBatchMode = is headless build = server)
            Debug.Log($"=== Client Build ==="); 
            networkManager.StartClient(); // use NetworkManager to start Client immediately
        } else {
            Debug.Log($"=== Server Build ===");
        }
    }

    // Used by Join-Button on click to connect to localhost
    public void JoinLocal() {
        networkManager.networkAddress = "localhost";
        networkManager.StartClient(); 
    }
    
}
