using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject muteButton = null;
    [SerializeField] private GameObject tutorialButton = null;

    [SerializeField] private GameObject canvasBackground = null;

    [SerializeField] private GameObject canvasPlayer = null;

    [SerializeField] private GameObject creditsWindow = null;
    private void Awake() {
        AudioManager.PlayMusic("BG");
        GameState.changeToCustomizationScreen();
        SwapSoundButtonGraphic();
        if(PlayerPrefs.GetInt("TutorialMainMenu") == 1){
            hideTutorial();
        }else{ tutorialButton.GetComponent<Animation>().Play(); }
    }

    private void SwapSoundButtonGraphic(){
        AudioManager.PlayMusic("ButtonUI");
        if( PlayerPrefs.GetInt("SoundEnabled") == 1 ){
            muteButton.transform.GetChild(0).GetComponent<Image>().enabled = false;
            muteButton.transform.GetChild(1).GetComponent<Image>().enabled = true;
        }else{
            muteButton.transform.GetChild(1).GetComponent<Image>().enabled = false;
            muteButton.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    public void LoadGame(){
        AudioManager.StopMusic("BG");
        AudioManager.PlayMusic("ButtonUI");
        SaveCustomization();
        SceneManager.LoadScene("LoadingScene");
        GameObject root = SceneManager.GetActiveScene().GetRootGameObjects()[0];
        LoadScript.nextSceneName = "GameScene";
    }

    private void SaveCustomization(){
        SaveBackgorund();
        SavePlayer();
        PlayerPrefs.Save();
    }

    private void SavePlayer(){
        float distance      = 10000;
        int   closest_index = 0;
        Vector2 canvasPosition = canvasPlayer.transform.GetComponent<RectTransform>().anchoredPosition;
        for( int i =0; i < canvasPlayer.transform.childCount - 1; i++){
            Vector2 childPosition = canvasPlayer.transform.GetChild(i).GetComponent<HolderController>().GetPosition();
            if( Vector2.Distance( childPosition, canvasPosition  ) < distance ){
                distance = Vector2.Distance( childPosition, canvasPosition  );
                closest_index = i;
            }
        }
        PlayerPrefs.SetInt("PlayerID", closest_index);
    }

    private void SaveBackgorund(){
        float distance      = 10000;
        int   closest_index = 0;
        Vector2 canvasPosition = canvasBackground.transform.GetComponent<RectTransform>().anchoredPosition;
        for( int i =0; i < canvasBackground.transform.childCount - 1; i++){
            Vector2 childPosition = canvasBackground.transform.GetChild(i).GetComponent<HolderController>().GetPosition();
            if( Vector2.Distance( childPosition, canvasPosition  ) < distance ){
                distance = Vector2.Distance( childPosition, canvasPosition  );
                closest_index = i;
            }
        }
        PlayerPrefs.SetInt("Background", closest_index);
    }


    public void showCredits(){
        AudioManager.PlayMusic("ButtonUI");
        creditsWindow.transform.GetComponent<Animation>().Play("GameOverMenu");
    }

    public void hideCredits(){
        AudioManager.PlayMusic("ButtonUI");
        creditsWindow.transform.position = new Vector3( 0, -1200, 0);
    }

    public void MuteSound(){
        
        if( AudioManager.isSoundMuted() ){
            AudioManager.EnableSounds();
            AudioManager.PlayMusic("BG");
        }else{
            AudioManager.MuteAllSounds();
        }
        SwapSoundButtonGraphic();
    }

    public void hideTutorial(){
        PlayerPrefs.SetInt("TutorialMainMenu", 1);
        tutorialButton.SetActive(false);
        tutorialButton.transform.GetChild(0).GetComponent<Text>().enabled = false;
        tutorialButton.transform.GetChild(1).GetComponent<Text>().enabled = false;
        PlayerPrefs.Save();
    }
}
