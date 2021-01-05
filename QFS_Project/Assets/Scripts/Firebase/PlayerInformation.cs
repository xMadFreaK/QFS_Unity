using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class PlayerInformation : MonoBehaviour {
    //AccountCreation
    public TMP_InputField email;                
    public TMP_InputField username;
    public TMP_InputField password;

    //SignIn
    public TMP_InputField SignInEmail;
    public TMP_InputField SignInPW;

    //InAppStatistics
    public TextMeshProUGUI playernameText;
    public TextMeshProUGUI gamesText;
    public TextMeshProUGUI winsText;
    public TextMeshProUGUI lossesText;
    public TextMeshProUGUI quantityLoggedInText;
    public TextMeshProUGUI clicksText;

    public PanelManager panelManager;

    User2 user = new User2();

    private string databaseURL = "https://qfs-project-8f937-default-rtdb.firebaseio.com/users";     //unique URL to connect to our realtime database
    private string AuthKey = "AIzaSyBIJQdnJD19yLV3RxVXYIxJfJS_SXsq5Hk";                             //unique WEB-API-Key

    public static fsSerializer serializer = new fsSerializer();


    public static int quantityLoggedIn;         //in DB: quantityLoggedIn
    public static string playerName;            //in DB: username        
    public static int games;                    //in DB: games
    public static int wins;                     //in DB: wins
    public static int losses;                   //in DB: losses
    public static int clicks;

    private string idToken;                 //Is always created when the user signs in
    public static string localId;           //The unique Id for each user, in DB localId

    private string getLocalId;

    //public void OnSubmit() {                //for button-Click Submit
    //    PostToDatabase();
    //}

    //public void OnGetScore() {
    //    GetLocalId();
    //}

    // Button-Click "Register" (Authentification)
    public void SignUpUserButton() {
        SignUpUser(email.text, username.text, password.text);       // parameters are the text from the InputFields
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

                panelManager = PanelManager.GetInstance();
                panelManager.SwitchCanvas(PanelType.LogInScreen);
            }).Catch(error => {
                Debug.Log(error);
            });
    }
    private void PostToDatabase(bool newUser = false) {
        User2 user = new User2();

        if (newUser) {
            user.zgames = 0;
            user.zwins = 0;
            user.zlosses = 0;
            user.yquantLoggedIn = 0;
        }
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }

    //Button-Click "SignIn" (Authentification)
    public void SignInUserButton() {
        SignInUser(SignInEmail.text, SignInPW.text);
    }

    //LogIn User
    private void SignInUser(string email, string password) {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, userData).Then(
            response => {
                idToken = response.idToken;
                localId = response.localId;

                panelManager = PanelManager.GetInstance();
                panelManager.SwitchCanvas(PanelType.MainScreen);
                GetUserInformation();
            }).Catch(error => {
                Debug.Log(error);
            });
    }

    private void GetUserInformation() {  //was ist, wenn wir hier alle User-Infos holen? -> Geil
        RestClient.Get<User2>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response => {
            playerName = response.userName;
            games = response.zgames;
            wins = response.zwins;
            losses = response.zlosses;
            quantityLoggedIn = response.yquantLoggedIn + 1;
            clicks = response.yClicks;
            playernameText.text = playerName;
            gamesText.text = "Spiele gesamt: " + games.ToString();
            winsText.text = "Siege: " + wins.ToString();
            lossesText.text = "Niederlagen: " + losses.ToString();
            quantityLoggedInText.text = "Eingeloggt: " + quantityLoggedIn.ToString() + "x";

            PostToDatabase();



        }).Catch(error => {
            Debug.Log(error);
        });
    }



    //get information about a certain user
    private void RetrieveFromDatabase() {
        RestClient.Get<User2>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response => {
            user = response;            //speichert den user oben im User2-Objekt mit seinen spezifischen Daten (deshalb getLocalId)
            //hier z.B. hat Methode zum Updaten Platz
        }).Catch(error => {
            Debug.Log(error);
        });
    }







    //private void GetLocalId() {
    //    RestClient.Get(databaseURL + ".json?auth=" + idToken).Then(response => {
    //        var username = getScoreText.text;

    //        fsData userData = fsJsonParser.Parse(response.Text);
    //        Dictionary<string, User1> users = null;
    //        serializer.TryDeserialize(userData, ref users);

    //        foreach (var user in users.Values) {
    //            if (user.userName == username) {
    //                getLocalId = user.localId;
    //                RetrieveFromDatabase();
    //                break;
    //            }
    //        }
    //    }).Catch(error => {
    //        Debug.Log(error);
    //    });
    //}
}