using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
[System.Serializable]
public class AudioFile
{
    public string audioName;
    public AudioClip audioClip;
    [Range(0f,1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;
    public bool isLooping;
    public bool playOnAwake;
}

  #region VARIABLES
    public static AudioManager instance;
    public AudioFile[] audioFiles;
    private float timeToReset;
    private bool timerIsSet = false;
    private string tmpName;
    private float tmpVol;
    private bool isLowered = false;
    private bool fadeOut = false;
    private bool fadeIn = false;
    private string fadeInUsedString;
    private string fadeOutUsedString;

    static private bool isMuted;
 #endregion
 
 
 // Use this for initialization
    void Awake(){
        if (instance == null){ instance = this;
        }else if (instance != this){ Destroy(gameObject);}
        DontDestroyOnLoad(gameObject);
    
        foreach (var s in audioFiles){
            s.source        = gameObject.AddComponent<AudioSource>();
            s.source.clip   = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop   = s.isLooping;
            if (s.playOnAwake){ s.source.Play();}
        }
    }
  #region METHODS

    public static bool isSoundMuted(){
        return isMuted; 
    }

    public static void EnableSounds(){
        isMuted = false;
        PlayerPrefs.SetInt("SoundEnabled", 0);
        PlayerPrefs.Save();
    }

    public static void MuteAllSounds(){
        isMuted = true;
        PlayerPrefs.SetInt("SoundEnabled", 1);
        PlayerPrefs.Save();
        foreach( AudioFile s in instance.audioFiles ){
            s.source.Stop();
        }
    }

    public static void PlayMusic(string name){
        if( isMuted ) return;
        AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if( !s.source.isPlaying ) s.source.Play();
    }
    public static void StopMusic(String name){
        AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
        s.source.Stop();
    }
    public static void PauseMusic(String name){
        AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
        s.source.Pause();
    }
    public static void UnPauseMusic(String name){
        AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
        s.source.UnPause();
    }
     public static void LowerVolume(String name, float _duration)
    {
        if (instance.isLowered == false){
            AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
            instance.tmpName = name;
            instance.tmpVol = s.volume;
            instance.timeToReset = Time.time + _duration;
            instance.timerIsSet = true;
            s.source.volume = s.source.volume / 3;
            instance.isLowered = true;
        }
    }

    void ResetVol(){
        AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == tmpName);
        s.source.volume = tmpVol;
        isLowered = false;
    }
    private void Update(){
        if (Time.time >= timeToReset && timerIsSet){
            ResetVol();
            timerIsSet = false;
        }
    }
  #endregion
}
