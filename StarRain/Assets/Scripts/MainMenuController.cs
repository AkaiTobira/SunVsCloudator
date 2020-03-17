using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject muteButton = null;

    private void Awake() {
        GameState.changeToCustomizationScreen();
        if( PlayerPrefs.GetInt("SoundEnabled") == 0 ){
            muteButton.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            muteButton.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }else{
            muteButton.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            muteButton.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        }
    }

    public void LoadGame(){
        SceneManager.LoadScene("LoadingScene");
        GameObject root = SceneManager.GetActiveScene().GetRootGameObjects()[0];
        LoadScript.nextSceneName = "GameScene";
    }

    public void MuteSound(){
        if( AudioManager.isSoundMuted() ){
            AudioManager.EnableSounds();
            AudioManager.PlayMusic("BG");
            muteButton.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            muteButton.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }else{
            AudioManager.MuteAllSounds();
            muteButton.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            muteButton.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        }
    }

}
