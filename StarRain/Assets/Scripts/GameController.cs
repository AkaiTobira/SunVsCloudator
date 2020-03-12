using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timer = 0;

    private float collectablePoints = 0;
    [SerializeField] const float POINTS_MULTIPLER = 5;

    [SerializeField] public GameObject HighscoreText;
    [SerializeField] public GameObject UiText;
    [SerializeField] public GameObject UiSummarizeScore;
    [SerializeField] public GameObject GameOverMenu;

    [SerializeField] public GameObject PauseMenu;

    [SerializeField] public GameObject DebbugerText;

    [SerializeField] public GameObject[] players;

    void Awake() {
        GameState.changeToGameScreen();
        GameOverMenu.transform.GetComponent<OverMenuController>().Hide();
        SetBackgound();
        LoadPlayer();
    }


    public void AddPoints(float points, int objectId){
        collectablePoints += points;
    }
    void SetBackgound(){
        int selected_background = PlayerPrefs.GetInt("Background");
        for( int i = 0; i < transform.GetChild(0).childCount; i ++){
            transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = i == selected_background;
        };
    }

    void LoadPlayer(){
        int player_id = PlayerPrefs.GetInt("PlayerID");
        GameObject new_child = Instantiate(players[player_id], 
                                            new Vector3( 0.0f, 0.0f, 0.0f), 
                                            Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.transform.GetComponent<PlayerController>().UpdateCameraProperties();
    }

    public void GameOver(){
        GameOverMenu.transform.GetComponent<OverMenuController>().Show();
        GameOverMenu.transform.GetComponent<Animation>().Play("GameOverMenu");
        GameState.endGame();
        UpdateHighscore();
    }

    private void UpdateHighscore(){
        int current_highscore = PlayerPrefs.GetInt("Highscore");
        int current_score     = (int)(timer * POINTS_MULTIPLER + collectablePoints);
        if( current_highscore < current_score ){
            HighscoreText.GetComponent<Text>().text = "NEW HIGHSCORE !!!";
        }else{
            HighscoreText.GetComponent<Text>().text = "HIGHSCORE : " +  current_highscore.ToString();
        }
        AchievmentMeasures.update_measure("points", (int)(timer * POINTS_MULTIPLER + collectablePoints));
    }

    public void OnPauseButton(){
        if( GameState.isGameOver()) return;
        if( GameState.isGameActive() ){
            PauseMenu.SetActive(true);
            GameState.pauseGame();
        }else{
            PauseMenu.SetActive(false);
            GameState.startGame();
        }
    }


    public void StartGame(){
       // if( GameState.isGamePaused() ) OnPauseButton(); 
        if( ! GameState.isWaitingForGameStart()) return;
        GameState.startGame();
        PauseMenu.SetActive(false);
        PauseMenu.transform.GetChild(0).GetComponent<Text>().text = "PAUSE";
        transform.GetChild(1).GetComponent<EnemiesController>().RunAllEnemies();
    }

    void Update()
    {
        if( ! GameState.isGameActive() ) return;
        timer += Time.deltaTime;
        UiText.GetComponent<Text>().text           = "Score : " + ((int)(timer * POINTS_MULTIPLER + collectablePoints)).ToString();
        UiSummarizeScore.GetComponent<Text>().text = "Score : " + ((int)(timer * POINTS_MULTIPLER + collectablePoints)).ToString();
     //   DebbugerText.GetComponent<Text>().text     = transform.GetChild(3).position.ToString() + " : " +  (-transform.GetChild(3).GetComponent<BaseController>().screenWidth*0.5f).ToString();
    }

}
