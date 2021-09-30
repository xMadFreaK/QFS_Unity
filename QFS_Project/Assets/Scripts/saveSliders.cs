using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class saveSliders : MonoBehaviour
{
    public Slider sFXSlider;
    public Slider musicSlider;

    void Awake()
    {
        /*Liest die letzten Einstellungen der Schieberegler vorm Beenden
        des Spiels und weißt diese Werte den beiden Schiebereglern wieder zu */
        sFXSlider.value = PlayerPrefs.GetFloat("SFXSlider value");
        musicSlider.value = PlayerPrefs.GetFloat("musicSlider value");
    }
    void Update()
    {
        //die Werte werden aktualisiert
        Savestuff.sfxvol = sFXSlider.value;
        Savestuff.musicvol = musicSlider.value;
        PlayerPrefs.SetFloat("SFXSlider value", sFXSlider.value);
        PlayerPrefs.SetFloat("musicSlider value", musicSlider.value);
    }
}