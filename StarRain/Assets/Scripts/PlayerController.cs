using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : BaseController
{

    private bool pausePlayer = false;
    [SerializeField] public bool enableGoodMode = false;

    override protected void Awake() {
        base.Awake();
        pausePlayer = true;
        BlockHere   = true;

    }

    override public void HandleDirectionChange(){
        HandleAndriodInput();
        HandleMouseInput();
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            transform.parent.GetComponent<GameController>().StartGame();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    override protected void Update() {
        base.Update();
        HandleDirectionChange();
    }


    private void HandleMouseInput(){

        if ( Input.GetKeyDown( KeyCode.Space )){
            enableGoodMode = !enableGoodMode;
        }

        if ( Input.GetMouseButton(0) ){
            transform.parent.GetComponent<GameController>().StartGame();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if( enableGoodMode ) return;
        transform.parent.GetComponent<GameController>().GameOver();
        Destroy(this.gameObject);
    }

}
