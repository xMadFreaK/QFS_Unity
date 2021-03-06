using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    
    public TMP_Text filetext;
    public Text filet;
    
    // Start is called before the first frame update
 
    public void updateQuizSelect()
    {

        //holder = filetext.text;
        GameUtility.xmlFileName = filet.text;
        //GameUtility.xmlFileName = QuizInputField.Text;
        print(filet.text);
        
    }
    // Update is called once per frame

}
