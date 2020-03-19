using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplayButton : MonoBehaviour
{
    public void replayGame(){
        AudioManager.PlayMusic("ButtonUI");
        LoadScript.nextSceneName = "GameScene";
        SceneManager.LoadScene("LoadingScene");
    }

    public void GoToMainMenu(){

        AudioManager.StopMusic("BG1");
        AudioManager.StopMusic("BG2");
        AudioManager.PlayMusic("ButtonUI");
        GameState.endGame();
        LoadScript.nextSceneName = "MainMenuScene2";
        SceneManager.LoadScene("LoadingScene");
    }


}
