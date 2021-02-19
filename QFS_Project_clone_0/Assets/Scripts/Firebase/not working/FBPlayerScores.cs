using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FullSerializer;
using UnityEngine.Serialization;

//Just a class for Testing!!!
public class FBPlayerScores : MonoBehaviour
{
    //these will be added to the GameObject/GameLogic
    public TextMeshProUGUI ScoreText;
    public TMP_InputField getScoreText;

    public TMP_InputField emailText;
    public TMP_InputField usernameText;
    public TMP_InputField passwordText;


    //for Register and SignIn Process InputFields


    private System.Random random = new System.Random();

    User user = new User();

    //will be set below in the 2 methods
    public static int playerScore;
    public static string playerName;

    //will be needed for authentication-response
    private string idToken;
    public static string localId;

    private string AuthKey = "AIzaSyBIJQdnJD19yLV3RxVXYIxJfJS_SXsq5Hk";
    private string DBurl = "https://qfs-project-8f937-default-rtdb.firebaseio.com/users";

    private string getLocalId;

    public static fsSerializer serializer = new fsSerializer();

    // Start is called before the first frame update
    void Start()
    {
        playerScore = random.Next(0, 101);
        ScoreText.text = "Score: " + playerScore;
    }

    public void ClickSubmit() {      
        PostToDatabase();
    }
    public void ClickGetScore() {
        GetLocalId();
    }

    // method to send data to our database
    private void PostToDatabase(bool emptyScore = false) {
        User user = new User();

        if (emptyScore) {
            user.userScore = 0;
        }
        RestClient.Put(DBurl + "/" + localId +".json?auth=" + idToken, user);     //"Subfolder"
    }


    private void RetrieveFromDatabase() {
        
        RestClient.Get<User>(DBurl + "/" + getLocalId + ".json?auth=" + idToken).Then(response => {
            user = response;
            UpdateScore();
        });
    }

    private void UpdateScore() {
        ScoreText.text = "Score: " + user.userScore;
    }

    //here starts the Authentification-Code
    //for registration the user has to give us email, username and password, therefore this 3 parameters
    private void RegisterUser(string email, string username, string password) {
        string userData = "{\"email\":\""+ email +"\",\"password\":\""+ password +"\",\"returnSecureToken\":true}"; //the auth-server wants email and password in form of JSON
        RestClient.Post<RegisterResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, userData).Then(response => { //when request is done, Firebase gives a response
            idToken = response.idToken;
            localId = response.localId;
            playerName = username;
            PostToDatabase(true);
        }).Catch(error => {
            Debug.Log(error);                                                                                                   //when not true response e.g. too short pw, then
        });                                                                                                                      //show the error (go into catch)
    }                                                                                                       

    //for signing in the user don´t have to give us his username, only email and password
    private void SignInUser(string email, string password) {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}"; 
        RestClient.Post<RegisterResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, userData).Then(response => { 
            localId = response.localId;                                                                                         
            idToken = response.idToken;
            GetUsername();
        }).Catch(error => {
            Debug.Log(error);                                                                                                   
        });
    }


    public void ClickRegister() {
        RegisterUser(emailText.text, usernameText.text, passwordText.text);
    }

    public void ClickSignIn() {
        SignInUser(emailText.text, passwordText.text);
    }

    private void GetUsername() {
        RestClient.Get<User>(DBurl + localId + ".json?auth=" + idToken).Then(response => {
            playerName = response.userName;
        });
    }

    private void GetLocalId() {
        RestClient.Get(DBurl + ".json?auth=" + idToken).Then(response => {
            var username = getScoreText.text;

            fsData userData = fsJsonParser.Parse(response.Text);        //parses the JSON
            Dictionary<string, User> users = null;

            serializer.TryDeserialize(userData, ref users);

            foreach(var user in users.Values) {
                if(user.userName == username) {
                    getLocalId = user.localID;
                    RetrieveFromDatabase();
                    break;
                }
            }
        });
    }
}
