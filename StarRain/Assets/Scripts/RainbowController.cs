using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainbowController : MonoBehaviour
{

    private bool[] accuiredElements = {false,false,false,false,false,false};
    private bool isRainbowReady  = false;
    private bool isRainbowActive = false;

    private const int PRESS_DISTANCE = 120;

    public void Awake() {
        clearRainbow();

    }

    public void activateRainbowElement( int elemnt_id ){
        if(isRainbowReady) return;
        accuiredElements[elemnt_id] = true;
        transform.GetChild(elemnt_id).GetComponent<Renderer>().enabled = true;
        if( IsRainbowReady() ){
            clearRainbow();
            transform.GetChild(6).GetComponent<Renderer>().enabled = true;
            isRainbowReady = true;
            transform.GetChild(6).GetComponent<Animator>().SetTrigger("Activate");
        }
    }

    private bool IsRainbowReady(){
        foreach( bool element in accuiredElements){
            if(!element ) return false;
        }
        return true;
    }

    private void clearRainbow(){
        for( int i = 0; i < 7; i++){
            transform.GetChild(i).GetComponent<Renderer>().enabled = false;
        }
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
            if( t.phase != TouchPhase.Began) return;
            if(  Vector3.Distance( mousePosition , transform.parent.position ) <  PRESS_DISTANCE) FireRainbow();
        }
    }

    void Update() {

        if ( Input.GetKeyDown( KeyCode.Space )){
            accuiredElements = new bool[]{true, true, true, true, true, true};
            activateRainbowElement(0);
        }

        if( !isRainbowReady ) return;
        HandleAndriodInput();
        HandleMouseInput();
    }

    public bool isActivated(){
       return isRainbowActive;
    }

    private void FireRainbow(){
        if( !isRainbowReady) return;
        isRainbowReady = false;
        isRainbowActive = true;
        AchievmentMeasures.update_measure("rainbow", 1);
        transform.GetChild(6).GetComponent<Animator>().ResetTrigger("Activate");
        transform.GetChild(6).GetComponent<Animator>().SetTrigger("Fire!!!");
        StartCoroutine(ResetFireTrigger());
    }

    IEnumerator ResetFireTrigger ()
    {
       yield return new WaitForSeconds(transform.GetChild(6).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
       transform.GetChild(6).GetComponent<Animator>().ResetTrigger("Fire!!!");
       accuiredElements = new bool[]{false,false,false,false,false,false};
       clearRainbow();
       isRainbowActive = false;
    }
    private void HandleMouseInput(){
        if ( Input.GetMouseButton(0) ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if( !Input.GetMouseButtonDown(0)) return;
            if(  Vector3.Distance( mousePosition , transform.parent.position ) <  PRESS_DISTANCE) FireRainbow();
        }
    }

}
