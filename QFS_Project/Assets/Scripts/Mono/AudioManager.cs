using System;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable()]
public class Sound
{
    #region Variables

    [SerializeField]    String              name            = String.Empty;
    public              String              Name            { get { return name; } }

    [SerializeField]    AudioClip           clip            = null;
    public              AudioClip           Clip            { get { return clip; } }
    
    public float Volume;
    
    public float Pitch;
    public bool Loop;
    
    
    
    [HideInInspector]
    public              AudioSource         Source          = null;
    

    #endregion

    public void Play ()
    {
        
        Source.clip = Clip;
        Source.volume = Volume;
        Source.pitch = 1;
        Source.loop = Loop;

        Source.Play();
    }
    public void Stop ()
    {
        Source.Stop();
    }
}
public class AudioManager : MonoBehaviour {

    #region Variables

    public static       AudioManager    Instance        = null;

    [SerializeField]    Sound[]         sounds          = null;
    [SerializeField]    AudioSource     sourcePrefab    = null;

    [SerializeField]    String          startupTrack    = String.Empty;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string gamemusicPref = "gamemusicPref";
    private static readonly string soundeffectPref = "soundeffectPref";
    private int firstPlayInt;
    public Slider gamemusicSlider, soundeffectSlider;
    private float gamemusicFloat, soundeffectFloat;
    
    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitSounds();
        UpdateGameSound();
        UpdateSoundEffects();
    } 
    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        
            firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        
        if (firstPlayInt == 0)
        {
            gamemusicFloat = .25f;
            soundeffectFloat = .75f;
            gamemusicSlider.value = gamemusicFloat;
            soundeffectSlider.value = soundeffectFloat;
            PlayerPrefs.SetFloat(gamemusicPref, gamemusicFloat);
            PlayerPrefs.SetFloat(soundeffectPref, soundeffectFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
            
        }
        else
        {
            gamemusicFloat = PlayerPrefs.GetFloat(gamemusicPref);
            gamemusicSlider.value = gamemusicFloat;
            soundeffectFloat = PlayerPrefs.GetFloat(soundeffectPref);
            soundeffectSlider.value = soundeffectFloat;
        }
        PlaySound(startupTrack);
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(gamemusicPref, gamemusicSlider.value);
        PlayerPrefs.SetFloat(soundeffectPref, soundeffectSlider.value);
    }
    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
    #endregion

    /// <summary>
    /// Function that is called to initializes sounds.
    /// </summary>
    void InitSounds()
    {
        foreach (var sound in sounds)
        {
            AudioSource source = (AudioSource)Instantiate(sourcePrefab, gameObject.transform);
            source.name = sound.Name;

            sound.Source = source;
        }
    }

    /// <summary>
    /// Function that is called to play a sound.
    /// </summary>
    public void PlaySound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.Play();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.PlaySound()");
        }
    }
    /// <summary>
    /// Function that is called to stop a playing sound.
    /// </summary>
    public void StopSound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.Stop();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.StopSound()");
        }
    }
    public void UpdateGameSound()
    {
        
        var Gamemusic = GetSound(startupTrack);
            Gamemusic.Volume = gamemusicSlider.value;
    }
    public void UpdateSoundEffects()
    {
        foreach (var sound in sounds)
        {
            if (sound != GetSound(startupTrack))
            {
                sound.Volume = soundeffectSlider.value;
            }
        }
    }


    #region Getters

    Sound GetSound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.Name == name)
            {
                return sound;
            }
        }
        return null;
    }
    
    #endregion
}