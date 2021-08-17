﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public PanelManager panelManager;
    // Loads Scene "Game" as additive Scene to "Menue" (= Main Scene)
    public void EnterGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    // Loads Scene "Lobby" as additive Scene to "Menue" (= Main Scene)
    public void EnterLobby()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
    }

    // Deletes additive scene and go back to previous scene
    public void EndGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.UnloadSceneAsync("Game");
    }

    // Deletes additive scene and go back to previous scene at certain panel
    public void EndLobby()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.UnloadSceneAsync("Lobby");
        
        panelManager = PanelManager.GetInstance();
        //lastActivePanel.gameObject.SetActive(false);
        panelManager.SwitchCanvas(PanelType.MainScreen);
    }
}
