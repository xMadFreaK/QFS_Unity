using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class NetworkManagerQFS : NetworkManager // our own manager. overrides mirrors default manager
{
    // [Scene]-tag for being able to drag in scene into the inspector
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")] // reference to prefab for network room player
    [SerializeField] private NetworkRoomPlayerQFS roomPlayerPrefab = null; // set player in room to null

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    // all prefabs/game objects need to be loaded from the game to the server. on start of the server, all objects in the file "Resources" are loaded in automatically
    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    // method to announce all prefabs in file "Resources" to the network instead of doing it yourself - client side
    public override void OnStartClient() {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");
        foreach (var prefab in spawnablePrefabs) {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    // is called when client connects to server
    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn) {
        if (numPlayers >= maxConnections) {
            conn.Disconnect();
            return;
        }
        // if player is not in the menu scene, disconnet them - players cannot join game in progress
        if (SceneManager.GetActiveScene().name != menuScene) {
            conn.Disconnect();
            return;
        }
    }

    // called on the server when client adds a new player with ClientSceneAddPlayer (in the OnClientConnect method)
    public override void OnServerAddPlayer (NetworkConnection conn) {
        if (SceneManager.GetActiveScene().name == menuScene) { // if we are in the menu scene, spawn in the room player prefab
            NetworkRoomPlayerQFS roomPlayerInstance = Instantiate(roomPlayerPrefab);
            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }
}
