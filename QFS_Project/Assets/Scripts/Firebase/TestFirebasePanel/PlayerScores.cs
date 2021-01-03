using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class PlayerScores : MonoBehaviour {
    public TextMeshProUGUI scoreText;

    public TMP_InputField getScoreText;                 //(Dein Nutzername)

    public TMP_InputField emailText;
    public TMP_InputField usernameText;
    public TMP_InputField passwordText;

    private System.Random random = new System.Random();  //Randomizer object for the simple algorithm in start function

    User1 user = new User1();

    private string databaseURL = "https://qfs-project-8f937-default-rtdb.firebaseio.com/users";     //unique URL to connect to our realtime database
    private string AuthKey = "AIzaSyBIJQdnJD19yLV3RxVXYIxJfJS_SXsq5Hk";                             //unique WEB-API-Key

    public static fsSerializer serializer = new fsSerializer();                                     


    public static int playerScore;          //only to put the random number in the first field scoreText
    public static string playerName;
    public static int matches;

    private string idToken;                 //Is always created when the user signs in
    public static string localId;           //The unique Id for each user

    private string getLocalId;

    private void Start() {
        playerScore = random.Next(0, 101);
        scoreText.text = "Score: " + playerScore;
    }

    public void OnSubmit() {                //for button-Click Submit
        PostToDatabase();
    }

    public void OnGetScore() {              
        GetLocalId();
    }

    private void UpdateScore() {
        scoreText.text = "Score: " + user.userScore;
    }

    private void PostToDatabase(bool emptyScore = false) {
        User1 user = new User1();

        if (emptyScore) {
            user.userScore = 0;
            user.matches = 10;
        }

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }

    private void RetrieveFromDatabase() {
        RestClient.Get<User1>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            user = response;
            UpdateScore();
        });
    }

    // Button-Click "Register" (Authentification)
    public void SignUpUserButton() {
        SignUpUser(emailText.text, usernameText.text, passwordText.text);       // parameters are the text from the InputFields
    }

    //Button-Click "SignIn" (Authentification)
    public void SignInUserButton() {
        SignInUser(emailText.text, passwordText.text);
    }

    // Register new User (Authentification)
    private void SignUpUser(string email, string username, string password) {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";             // in JSON-format
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, userData).Then(   // Register the User to Firebase
            response => { // response is a SignResponse object, FireBase gives us this response
                idToken = response.idToken;                         // everytime generated when the users signs in
                localId = response.localId;                         // unique UserId
                playerName = username;                              
                PostToDatabase(true);                               //when register a new user parameter true for the method, so we know its a new user
                                                                    // (Realtime-Database)
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
    //LogIn User
    private void SignInUser(string email, string password) {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, userData).Then(
            response => {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
                //Here missing matches, wins, etc.
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    private void GetUsername() {
        RestClient.Get<User1>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response => {
            playerName = response.userName;
        });
    }

    private void GetLocalId() {
        RestClient.Get(databaseURL + ".json?auth=" + idToken).Then(response => {
            var username = getScoreText.text;

            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, User1> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values) {
                if (user.userName == username) {
                    getLocalId = user.localId;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error => {
            Debug.Log(error);
        });
    }
}