using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//is used for the response from Firebase when a new User registers

[Serializable]
public class SignResponse {
    public string localId;
    public string idToken;
}