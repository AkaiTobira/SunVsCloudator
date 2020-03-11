using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected float m_speed = 100.0f;
    protected Rigidbody2D m_rigidbody;
    protected Vector2     m_direction;

    protected float screenHight;
    protected float screenWidth;


    protected bool sortY = true;
    [HideInInspector] public int index;
    [HideInInspector] public int numberOfChild;

    [SerializeField] protected float timerStep = 0.2f;
    protected float timer = 0.0f;
    public virtual void Awake() {
        m_rigidbody  = GetComponent<Rigidbody2D>();
        m_direction  = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0 ).normalized;
        timer        = timerStep;
    }

    protected virtual void AdaptAnimation(){}
    public virtual void ChangeDirection(){}

    //TO change
    protected virtual void TeleportByWall(){
        
        if( transform.position.x < -screenWidth*0.5f ){ 
            transform.position = new Vector3(  screenWidth*0.5f, transform.position.y ); 

            }
        if( transform.position.x >  screenWidth*0.5f ){
             transform.position = new Vector3( -screenWidth*0.5f, transform.position.y ); 

             }
        if( transform.position.y < -screenHight*0.5f ){ 
            transform.position = new Vector3( transform.position.x, screenHight*0.5f ); 

        }
        if( transform.position.y >  screenHight*0.5f ){ 
            transform.position = new Vector3( transform.position.x, -screenHight*0.5f ); 

        }
    }

    private void Move(){
        if( sortY) GetComponent<Renderer>().sortingOrder = (int)transform.position.y;
        
     //   gameObject.layer = 
        m_rigidbody.velocity = new Vector2( m_direction.x * m_speed, m_direction.y * m_speed );
        if(!GameState.isGameActive()) m_rigidbody.velocity = new Vector2(0.0f,0.0f);
    }

    protected virtual void Update(){
        if( GameState.isGameActive() ) UpdateTimer();
        ChangeDirection();
        AdaptAnimation();
        Move();
        TeleportByWall();
    }

    public virtual void OnStart(){}

    public virtual void UpdateTimer(){}

    public void SetScreenSize( float x, float y){
        screenHight = x*2;
        screenWidth = y*2;
    }

}
