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

    [SerializeField] public GameObject NewPlayerBanner;
    [SerializeField] public GameObject NewBackGroundBanner;
    [SerializeField] public GameObject[] players;

   [SerializeField] public GameObject MainMenuButton;

   [SerializeField] public GameObject StartGameButton;

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
        AudioManager.PlayMusic("BG" + (selected_background+1).ToString() );
        for( int i = 0; i < transform.GetChild(0).childCount; i ++){
            if( i == selected_background ) continue;
            transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = false;
            transform.GetChild(0).GetChild(i).transform.position = new Vector3( -10000, -100000, 0);
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
        PlayerPrefs.SetInt("RainbowBeamEnabled", 0);
        UpdateHighscore();
        processUnlockBanners();

        UiText.GetComponent<Text>().text= "";

        int selected_background = PlayerPrefs.GetInt("Background");
        AudioManager.StopMusic("BG" + (selected_background+1).ToString() );
    }

    private void processUnlockBanners(){

        if( AchievmentMeasures.unlocked_new_background() ) {
            NewBackGroundBanner.transform.GetComponent<Animation>().Play("S1");
            AudioManager.PlayMusic("Achievment");
        };
        if( AchievmentMeasures.unlocked_new_player() ) {
            NewPlayerBanner.transform.GetComponent<Animation>().Play("S2");
            AudioManager.PlayMusic("Achievment");        
        };
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
        AudioManager.PlayMusic("ButtonUI");
        if( GameState.isWaitingForGameStart() ){
            StartGame();
            return;
        }
        if( GameState.isGameOver()) return;
        if( GameState.isGameActive() ){
            enablePause();
        }else{
            disabelPause();
        }
    }

    private void enablePause(){
            PauseMenu.SetActive(true);
            GameState.pauseGame();
            PauseMenu.transform.GetChild(2).GetComponent<PauseMenuSunnies>().SetActive();

            enableMainMenuButton();
            transform.GetChild(3).GetComponent<Animator>().SetBool("goSleep", true);
    }

    private void disabelPause(){
            PauseMenu.SetActive(false);
            GameState.startGame();
            disableMainMenuButton();
            transform.GetChild(3).GetComponent<Animator>().SetBool("goSleep", false);
    }

    private void disableMainMenuButton(){
        MainMenuButton.GetComponent<Button>().enabled = false;
        MainMenuButton.GetComponent<Image>().enabled = false;
        MainMenuButton.transform.GetChild(0).GetComponent<Text>().enabled = false;
    }



    private void enableMainMenuButton(){
        MainMenuButton.GetComponent<Button>().enabled = true;
        MainMenuButton.GetComponent<Image>().enabled = true;
        MainMenuButton.transform.GetChild(0).GetComponent<Text>().enabled = true;
    }

    public void StartGame(){
        if( ! GameState.isWaitingForGameStart()) return;
        AudioManager.PlayMusic("ButtonUI");
        GameState.startGame();
        PauseMenu.SetActive(false);
        disableStartGameButton();
        PauseMenu.transform.GetChild(0).GetComponent<Text>().text = "PAUSE";
        disableMainMenuButton();
    }

    private void disableStartGameButton(){
        StartGameButton.transform.GetComponent<Button>().enabled = false;
        StartGameButton.transform.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if( ! GameState.isGameActive() ) return;
        timer += Time.deltaTime;
        UiText.GetComponent<Text>().text           = "Score : " + ((int)(timer * POINTS_MULTIPLER + collectablePoints)).ToString();
        UiSummarizeScore.GetComponent<Text>().text = "Score : " + ((int)(timer * POINTS_MULTIPLER + collectablePoints)).ToString();
    }

}
