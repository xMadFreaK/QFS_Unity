using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Auswahl : MonoBehaviour
{
    public static string catTopic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        catTopic = gameObject.name;
        SceneManager.LoadScene("Quizscene");
    }
}
