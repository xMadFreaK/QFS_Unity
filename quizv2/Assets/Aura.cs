using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NextButton.resetAura == "y")
        {
            GetComponent<ParticleSystem>().Stop();
        }
        if(NextButton.resetAura == "n")
        {
            GetComponent<ParticleSystem>().Play();

        }
    }
}
