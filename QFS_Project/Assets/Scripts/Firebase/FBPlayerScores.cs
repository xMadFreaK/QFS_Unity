using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Just a class for Testing!!!
public class FBPlayerScores : MonoBehaviour
{
    // these will be added to the GameObject/GameLogic
    public TextMeshProUGUI ScoreText;
    public TMP_InputField usernameText;

    private System.Random random = new System.Random();

    User user = new User();

    // will be set below in the 2 methods
    public static int playerScore;
    public static string playerName;



    // Start is called before the first frame update
    void Start()
    {
        playerScore = random.Next(0, 101);
        ScoreText.text = "Score: " + playerScore;
    }

    public void ClickSubmit() {
        playerName = usernameText.text;
        PostToDatabase();
    }

    // method to send data to our database
    private void PostToDatabase() {

        User user = new User();
        RestClient.Put("https://qfs-project-8f937-default-rtdb.firebaseio.com/"+ playerName +".json", user);     //"Subfolder"
    }

    public void ClickGetScore() {
        RetrieveFromDatabase();
    }

    private void UpdateScore() {
        ScoreText.text = "Score: " + user.userScore;
    }

    private void RetrieveFromDatabase() {
        RestClient.Get<User>("https://qfs-project-8f937-default-rtdb.firebaseio.com/" + usernameText.text + ".json").Then(response => {
            user = response;
            UpdateScore();
        });
        
    }


}
