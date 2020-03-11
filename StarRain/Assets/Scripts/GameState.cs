using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public enum GameStates {
        MainMenuOpen,
        CustomizationOpen,
        GamePrepare,
        GameActive,
        GamePaused,
        GameOver
    };
    private static GameStates currentState = GameStates.MainMenuOpen;


    static public void changeToMainMenu(){
        currentState = GameStates.MainMenuOpen;
    }


    static public void changeToCustomizationScreen(){
        currentState = GameStates.CustomizationOpen;
    }

    static public void changeToGameScreen(){
        currentState = GameStates.GamePrepare;
    }

    static public void startGame(){
        currentState = GameStates.GameActive;
    }

    static public void pauseGame(){
        currentState = GameStates.GamePaused;
    }

    static public void endGame(){
        currentState = GameStates.GameOver;
    }

    static public bool isGameOver(){
        return currentState == GameStates.GameOver;
    }

    static public bool isWaitingForGameStart(){
        return currentState == GameStates.GamePrepare;
    }

    static public bool isGameActive(){
        return currentState == GameStates.GameActive;
    }
}