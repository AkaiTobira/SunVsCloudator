using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSunnies : MonoBehaviour
{

    private bool isActive = false;
    private bool isTapped = false;
    [SerializeField] private GameObject gameControllNode;

    const int PRESS_DISTANCE = 200;

    void Awake()
    {
        disableSleepySunny();
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void enableSleepySunny(){
        GetComponent<SpriteRenderer>().enabled = true;
        int player_id = PlayerPrefs.GetInt("PlayerID") + 1;
        GetComponent<Animator>().SetInteger("sleepingSunny", player_id);
    }

    private void disableSleepySunny(){
        GetComponent<Animator>().SetInteger("sleepingSunny", 0);
    }

    public void SetActive(){
        if( !GameState.isGamePaused() ) return;
        isActive = true;
        isTapped = false;
        enableSleepySunny();
        StartCoroutine(TurnOnText());
    }

    public void SetDeactive(){
        if( !GameState.isGamePaused() ) return;
        isActive = false;
        GetComponent<Animator>().SetBool("wakeUp", false);
        disableSleepySunny();
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void HandleMouseInput(){
        if ( Input.GetMouseButton(0) ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if( !Input.GetMouseButtonDown(0)) return;
            print( Vector3.Distance( mousePosition , transform.parent.position ) );
            if(  Vector3.Distance( mousePosition , transform.parent.position ) <  PRESS_DISTANCE) StartCoroutine(WakieWakie());
        }
    }

    private void HandleAndriodInput(){
        foreach( Touch t in Input.touches ){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(t.position);
            if( t.phase != TouchPhase.Began) return;
            if(  Vector3.Distance( mousePosition , transform.parent.position ) <  PRESS_DISTANCE) StartCoroutine(WakieWakie());
        }
    }

    IEnumerator WakieWakie ()
    {
        isTapped = true;
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<Animation>().Stop();
        GetComponent<Animator>().SetBool("wakeUp", true);
        yield return new WaitForSeconds(1.5f);
        if( !isActive ){
            SetDeactive();
            yield break;
        }
        SetDeactive();
        gameControllNode.GetComponent<GameController>().OnPauseButton();
    }

    IEnumerator TurnOnText ()
    {
       yield return new WaitForSeconds(3.0f);
       if( !isActive ) yield break;
       if( isTapped  ) yield break;
       transform.GetChild(0).GetComponent<Animation>().wrapMode = WrapMode.Loop;
       transform.GetChild(0).GetComponent<Animation>().Play();
    }

    void Update()
    {
        if( isActive ){
            HandleMouseInput();
            HandleAndriodInput();
        }
    }
}
