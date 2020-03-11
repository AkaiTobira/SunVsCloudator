using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : BaseController
{

    [SerializeField] public bool blockInput  = false;
    [SerializeField] public bool goodMode    = false;

    private int flipTimes = 0;

    override public void Awake(){
        base.Awake();
        GetComponent<Renderer>().sortingOrder = -1999;
        sortY = false;
    }

    override public void ChangeDirection(){
        if( GameState.isGameActive() || GameState.isWaitingForGameStart() ){
            HandleAndriodInput();
            HandleMouseInput();
        }else{
            m_direction = new Vector3(0.0f,0.0f,0.0f);   
        }
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            transform.parent.GetComponent<GameController>().StartGame();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
            if( transform.position.y > 150 && mousePosition.y > 150){ return; }
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    override protected void Update() {
        base.Update();
        ChangeDirection();
    }


    private void HandleMouseInput(){

        if ( Input.GetKeyDown( KeyCode.Space )){
            goodMode = !goodMode;
        }

        if ( Input.GetMouseButton(0) ){
            transform.parent.GetComponent<GameController>().StartGame();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if( goodMode ) return;
        transform.parent.GetComponent<GameController>().GameOver();
        AchievmentMeasures.update_measure("byWall", flipTimes);
        m_speed = 0.0f;
    }

    public void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screenHight = 2f * cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    //TO change
    override protected void TeleportByWall(){
        if( transform.position.x < -screenWidth*0.5f ){ 
            transform.position = new Vector3(  screenWidth*0.5f, transform.position.y ); 
            flipTimes++;
            }
        if( transform.position.x >  screenWidth*0.5f ){
             transform.position = new Vector3( -screenWidth*0.5f, transform.position.y ); 
             flipTimes++;
             }
        if( transform.position.y < -screenHight*0.5f ){ 
            transform.position = new Vector3( transform.position.x, screenHight*0.5f ); 
             flipTimes++;
        }
        if( transform.position.y >  screenHight*0.5f ){ 
            transform.position = new Vector3( transform.position.x, -screenHight*0.5f ); 
             flipTimes++;
        }
    }

}
