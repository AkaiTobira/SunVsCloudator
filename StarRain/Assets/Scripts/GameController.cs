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
    public float timer = 0;
    [SerializeField] const float pointsMultiplyer = 5;

    [SerializeField] public GameObject HighscoreText;
    [SerializeField] public GameObject UiText;
    [SerializeField] public GameObject UiSummarizeScore;
    [SerializeField] public GameObject GameOverMenu;

    [SerializeField] public GameObject DarkImage;

    [SerializeField] public GameObject[] players;

    void Awake() {
        GameOverMenu.transform.GetComponent<OverMenuController>().Hide();
        currentState = GameState.WaitForPlayer;
        SetBackgound();
    }

    void SetBackgound(){
        int selected_background = PlayerPrefs.GetInt("Background");
        for( int i = 0; i < transform.GetChild(0).childCount; i ++){
            transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = i == selected_background;
        };
        int selected_player = PlayerPrefs.GetInt("PlayerID");
        loadPlayer(selected_player);
    }

    void loadPlayer( int player_id){
        GameObject new_child = Instantiate(players[player_id], 
                                            new Vector3( 0.0f, 0.0f, 0.0f), 
                                            Quaternion.identity);
        new_child.transform.parent = this.transform;
    }


    public void GameOver(){
        GameOverMenu.transform.GetComponent<OverMenuController>().Show();
        GameOverMenu.transform.GetComponent<Animation>().Play("GameOverMenu");
        currentState = GameState.Over;
        UpdateHighscore();
    }

    private void UpdateHighscore(){
        int current_highscore = PlayerPrefs.GetInt("Highscore");
        int current_score     = (int)(timer * pointsMultiplyer);
        bool new_highscore    = current_highscore < current_score;
        if( new_highscore ){
            HighscoreText.GetComponent<Text>().text = "NEW HIGHSCORE !!!";
        }else{
            HighscoreText.GetComponent<Text>().text = "HIGHSCORE : " +  current_highscore.ToString();
        }
        AchievmentMeasures.update_measure("points", (int)(timer * pointsMultiplyer));
    }

    public void StartGame(){
        if( currentState != GameState.WaitForPlayer ) return;
        currentState = GameState.Play;
        transform.GetChild(2).GetComponent<EnemiesController>().RunAllEnemies();
        DarkImage.SetActive(false);
    }

    public bool isGameStarted(){
        return currentState == GameState.Play;
    }

    void BlockPlayer(){
        if( currentState != GameState.Play ) {
            BaseController player_script = transform.GetChild(4).GetComponent<BaseController>();
            if( player_script ) player_script.BlockHere = true;
        }
    }

    void Update()
    {
        BlockPlayer();
        if( currentState != GameState.Play ) return;
        timer += Time.deltaTime;
        UiText.GetComponent<Text>().text           = "Score : " + ((int)(timer * pointsMultiplyer)).ToString();
        UiSummarizeScore.GetComponent<Text>().text = "Score : " + ((int)(timer * pointsMultiplyer)).ToString();
    }

}
