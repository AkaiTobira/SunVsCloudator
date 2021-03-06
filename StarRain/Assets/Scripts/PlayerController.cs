﻿using System.Collections;
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

        if( GameState.isGameActive() || GameState.isWaitingForGameStart() || GameState.isGamePaused() ){
            HandleAndriodInput();
            HandleMouseInput();
        }else{
            m_direction = new Vector3(0.0f,0.0f,0.0f);   
        }
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if( transform.GetChild(0).GetComponent<RainbowController>().isActivated() ) return;

        if( col.gameObject.tag == "Collectable" ){
            float additionalPoints = col.gameObject.GetComponent<CollectableObject>().points;
            int objectId           = col.gameObject.GetComponent<CollectableObject>().objectId;

            transform.parent.GetComponent<GameController>().AddPoints(additionalPoints, objectId );
            transform.GetChild(0).GetComponent<RainbowController>().activateRainbowElement(objectId);
        }else if(col.gameObject.tag == "Killer"){
            print( "SMT");
        }else{
            if( goodMode ) return;
            if( GameState.isGameActive() ){
                GetComponent<Animator>().SetTrigger("isDead");
                AchievmentMeasures.update_measure("byWall", flipTimes);
                transform.parent.GetComponent<GameController>().GameOver();
                m_speed = 0.0f;
                AudioManager.PlayMusic("GameOver");
            }
        }
    }

    public void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screenHight = 2f * cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    override protected void TeleportByWall(){

        if( Mathf.Abs(transform.position.x) > screenWidth*0.5f ){
            float sign     = transform.position.x/Mathf.Abs(transform.position.x);
            float distance = (screenWidth*0.5f - 5);

            transform.position = new Vector3( -1 * sign * distance, transform.position.y ); 
            flipTimes++;
        }

        if( Mathf.Abs(transform.position.y) > screenHight*0.5f ){
            float sign     = transform.position.y/Mathf.Abs(transform.position.y);
            float distance = (screenHight*0.5f - 5);

            transform.position = new Vector3( transform.position.x, -1 * sign * distance  ); 
            flipTimes++;
        }
    }

}
