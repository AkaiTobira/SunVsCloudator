using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

    Animator anim;

    void Start() {
        PlayerPrefs.DeleteAll();
        if( PlayerPrefs.GetInt("SoundEnabled") == 1 ) AudioManager.MuteAllSounds(); 
        anim = GetComponent<Animator>();
    }

    private void GoToNextScreen(){
        AudioManager.PlayMusic("ButtonUI");
        SceneManager.LoadScene("LoadingScene");
        LoadScript.nextSceneName = "MainMenuScene2";
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
           GoToNextScreen();
        }
    }

    private void HandleMouseInput(){
        if ( Input.GetMouseButton(0) ){
            GoToNextScreen();
        }
    }
    void Update()
    {
        HandleMouseInput();
        HandleAndriodInput();
    }
}
