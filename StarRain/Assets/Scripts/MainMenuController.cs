using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject background = null;
    [SerializeField] private GameObject player = null;
    public void LoadGame(){
        if( !background.GetComponent<TimeAchivement>().is_current_backgorund_valid() ) return;
        if( !player.GetComponent<PlayerAchivement>().is_current_backgorund_valid() ) return;
        SceneManager.LoadScene("LoadingScene");
        GameObject root = SceneManager.GetActiveScene().GetRootGameObjects()[0];
        LoadScript.nextSceneName = "GameScene";
        SaveSettings();
    }

    public void SaveSettings(){
        PlayerPrefs.SetInt("Background", background.GetComponent<TimeAchivement>().backgorund_index);
        PlayerPrefs.SetInt("PlayerID",   player.GetComponent<PlayerAchivement>().backgorund_index);
        PlayerPrefs.Save();
    }

    public void Exit(){
         Application.Quit();
    }


}
