using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Player player;

    // Get all sorts of information from the Player.cs script
    public void SetPlayer(Player _player) {
        this.player = _player;
        text.text = _player.onlineName;
        // text.text = "Player " + player.playerIndex.ToString();
    }

}
