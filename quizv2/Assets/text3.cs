using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text3 : MonoBehaviour
{
    List<string> thirdchoice = new List<string>() { "Muenchen","Boston","Marseille","Innsbruck", "Schio", "iaah", "summ,summ","blub","schnatter","zirp" };
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = thirdchoice[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (textcontrol.randQuestion > -1)
        {
            GetComponent<TextMesh>().text = thirdchoice[textcontrol.randQuestion];
        }
    }
    void OnMouseDown()
    {
        textcontrol.selectedAnswer = gameObject.name;
        textcontrol.choiceSelected = "y";
    }
}
