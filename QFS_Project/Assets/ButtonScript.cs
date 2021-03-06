using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI filetext;
    public string holder = "Q1.xml";
    // Start is called before the first frame update
 
    public void updateQuizSelect()
    {
        //holder = filetext.text;
        GameUtility.xmlFileName = holder;
        //GameUtility.xmlFileName = QuizInputField.Text;
    }
    // Update is called once per frame

}
