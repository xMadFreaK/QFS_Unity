using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text4 : MonoBehaviour
{
    List<string> fourthchoice = new List<string>() { "Hannover","Gettysburg","Cherbourg","Linz","Buja","gack","krah","roaar","pieps","bsss" };
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = fourthchoice[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (textcontrol.randQuestion > -1)
        {
            GetComponent<TextMesh>().text = fourthchoice[textcontrol.randQuestion];
        }
    }
    void OnMouseDown()
    {
        textcontrol.selectedAnswer = gameObject.name;
        textcontrol.choiceSelected = "y";
    }
}
