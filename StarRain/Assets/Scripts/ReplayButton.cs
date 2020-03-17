using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplayButton : MonoBehaviour
{
    public void replayGame(){
        LoadScript.nextSceneName = "GameScene";
        SceneManager.LoadScene("LoadingScene");
    }

    public void GoToMainMenu(){
        LoadScript.nextSceneName = "MainMenuScene2";
        SceneManager.LoadScene("LoadingScene");
    }


}
