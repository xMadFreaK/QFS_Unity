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
        sFXSlider.value = PlayerPrefs.GetFloat("SFXSlider value");
        musicSlider.value = PlayerPrefs.GetFloat("musicSlider value");
    }
    // Use this for initialization
    void Start()
    {
        Savestuff.sfxvol = sFXSlider.value;
        Savestuff.musicvol = musicSlider.value;

    }
    void Update()
    {
        Savestuff.sfxvol = sFXSlider.value;
        Savestuff.musicvol = musicSlider.value;
        PlayerPrefs.SetFloat("SFXSlider value", sFXSlider.value);
        PlayerPrefs.SetFloat("musicSlider value", musicSlider.value);
    }

}