using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(){
        SceneManager.LoadScene("LoadingScene");
        GameObject root = SceneManager.GetActiveScene().GetRootGameObjects()[0];
        LoadScript.nextSceneName = "GameScene";
    }

    public void Exit(){

    }


}
