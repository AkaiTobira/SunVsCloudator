using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{

    [SerializeField] public bool enableGoodMode = false;
    override public void HandleDirectionChange(){
        HandleAndriodInput();
        HandleMouseInput();
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    private void HandleMouseInput(){
        if ( Input.GetMouseButton(0) ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_direction   = (mousePosition - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if( enableGoodMode ) return;
        SceneManager.LoadScene(0);
    }

}
