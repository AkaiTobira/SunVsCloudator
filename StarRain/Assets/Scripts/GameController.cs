using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{



    public GameState currentState = GameState.WaitForPlayer;
    public enum GameState {
        WaitForPlayer, Play, Over
    };
    float timer = 0;
    [SerializeField] const float pointsMultiplyer = 5;
    [SerializeField] public GameObject UiText;
    [SerializeField] public GameObject UiSummarizeScore;
    [SerializeField] public GameObject GameOverMenu;

    [SerializeField] public GameObject DarkImage;


    void Awake() {
        GameOverMenu.transform.GetComponent<OverMenuController>().Hide();
        Camera.main.aspect = 1.77f;
        currentState = GameState.WaitForPlayer;
    }

    public void GameOver(){
        GameOverMenu.transform.GetComponent<OverMenuController>().Show();
        currentState = GameState.Over;
    }

    public void StartGame(){
        if( currentState != GameState.WaitForPlayer ) return;
        currentState = GameState.Play;
        transform.GetChild(3).GetComponent<EnemiesController>().RunAllEnemies();
        DarkImage.SetActive(false);
    }

    public bool isGameStarted(){
        return currentState == GameState.Play;
    }

    void BlockPlayer(){
        if( currentState != GameState.Play ) {
            BaseController player_script = transform.GetChild(2).GetComponent<BaseController>();
            if( player_script ) player_script.BlockHere = true;
        }
    }

    void Update()
    {
        BlockPlayer();
        if( currentState != GameState.Play ) return;
        timer += Time.deltaTime;
        UiText.GetComponent<Text>().text           = "Points : " + ((int)(timer * pointsMultiplyer)).ToString();
        UiSummarizeScore.GetComponent<Text>().text = "Points : " + ((int)(timer * pointsMultiplyer)).ToString();
    }

}
