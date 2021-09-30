using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Savestuff
{
    // Start is called before the first frame update
    public static float sfxvol;
    public static float musicvol;

    public static string lastscene = "Menue";

    public static int seed;

    public static int CreateSeed(string _matchID) {
        string oneNumber;
        string GeneratedNumber = "";
        int seed;
        int counter = 1;

        foreach(char c in _matchID) {
            if (counter < 5) {
                oneNumber = System.Convert.ToInt32(c).ToString();
                GeneratedNumber = GeneratedNumber + oneNumber;
                counter++;
            } else {
                break;
            }
        }
        seed = Convert.ToInt32(GeneratedNumber);

        return seed;
    }

}
