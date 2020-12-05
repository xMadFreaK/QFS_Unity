using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour // sitting on UI where player enters name
{
    
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null; // sets name field
    [SerializeField] private Button continueButton = null; // sets continue-button
    
        
    public static string DisplayName { get; private set; } // name can be grabbed from somewhere but cannot be changed ("set")
    // PlayerNameInput.DisplayName - access to name without needing a reference, as it is static
    private const string PlayerPrefsNameKey = "PlayerName"; // name can be saved in the player press

    private void Start() => SetUpInputField(); // on start the method to set up the input field is called

    // method to set up the input field
    private void SetUpInputField() { 
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) {return;} // check if there is no name saved yet. if not, then return
        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey); // if there is a name saved, the name string is grabbed
        nameInputField.text = defaultName; // sets saved name to be input field
        SetPlayerName(defaultName);
    }

    // method to turn the continue button on and off, based on whether a string has been entered into name field
    public void SetPlayerName(string name) {
        continueButton.interactable = !string.IsNullOrEmpty(name); // checks if valid string has been entered to name field
    }

    // the player name is saved (triggered by pressing the continue button)
    public void SavePlayerName() {
        DisplayName = nameInputField.text; // the static string DisplayName is set to the text entered into name input field
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName); // text is saved to player prefs
    }
    
}
