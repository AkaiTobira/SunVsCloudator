using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollowController : MonoBehaviour
{
    [SerializeField] float m_speed = 50.0f;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_direction;

    private float screen_hight;
    private float screen_width;

    [HideInInspector] public int index;
    [HideInInspector] public int numberOfChild;


    [SerializeField] private float timerStep = 0.2f;
    private float timer = 0.0f;
    private void Awake() {
        m_rigidbody   = GetComponent<Rigidbody2D>();
        timer = timerStep;
    }

    private void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = 2f * cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;
    }

    private void HandleInput(){

        
        m_direction = (transform.parent.Find("Player").position - transform.position).normalized;
        m_rigidbody.velocity = new Vector2( m_direction.x * m_speed, m_direction.y * m_speed );
    }

    private void GotThroughtWall(){
        if( transform.position.x < -screen_width*0.5f ){ transform.position = new Vector3(  screen_width*0.5f, transform.position.y ); }
        if( transform.position.x >  screen_width*0.5f ){ transform.position = new Vector3( -screen_width*0.5f, transform.position.y ); }
        if( transform.position.y < -screen_hight*0.5f ){ transform.position = new Vector3( transform.position.x,  screen_hight*0.5f ); }
        if( transform.position.y >  screen_hight*0.5f ){ transform.position = new Vector3( transform.position.x, -screen_hight*0.5f ); }
    }

    void Update(){
        UpdateCameraProperties();
        HandleInput();
        GotThroughtWall();
        UpdateTimer();
    }

    void UpdateTimer(){
        timer -= Time.deltaTime;
        if( timer < 0 ){
        //    / index =  (index + 1) % numberOfChild;
            timer += timerStep; 
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.name == "Player"){
            SceneManager.LoadScene(0);
        }
       // Debug.Log(gameObject.name + " triggered " + col.gameObject.name); 
    }

}

