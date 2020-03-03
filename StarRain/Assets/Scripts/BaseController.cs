using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected float m_speed = 100.0f;
    protected Rigidbody2D m_rigidbody;
    protected Vector2 m_direction;

    [SerializeField] public bool BlockHere = true;

    protected float screen_hight;
    protected float screen_width;

    [HideInInspector] public int index;
    [HideInInspector] public int numberOfChild;

    [SerializeField] protected float timerStep = 0.2f;
    protected float timer = 0.0f;
    protected virtual void Awake() {
        BlockHere = true;
        m_rigidbody   = GetComponent<Rigidbody2D>();
        m_direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0 ).normalized ;
        timer = timerStep;
    }
    protected void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = 2f * cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;
    }

    protected virtual void flipAnimation(){
            GetComponent<SpriteRenderer>().flipX = m_direction.x > 0;
    }
    public virtual void HandleDirectionChange(){
            m_direction  = new Vector3(1,0,0);
    }
    protected virtual void TeleportByWall(){
        
        if( transform.position.x < -screen_width*0.5f ){ transform.position = new Vector3(  screen_width*0.5f, transform.position.y ); }
        if( transform.position.x >  screen_width*0.5f ){ transform.position = new Vector3( -screen_width*0.5f, transform.position.y ); }
        if( transform.position.y < -screen_hight*0.5f ){ 
            if( m_direction.y < 0 ) m_direction = new Vector3( m_direction.x, -m_direction.y ); }
        if( transform.position.y >  150 ){ 
            if( m_direction.y > 0 ) m_direction = new Vector3( m_direction.x, -m_direction.y ); }
    }

    private void move(){
        m_rigidbody.velocity = new Vector2( m_direction.x * m_speed, m_direction.y * m_speed );
    }


    private bool startBlock(){
        if( BlockHere ) {
            timer += Time.deltaTime;
            if(timer > 1.0){
                timer = 0;
                BlockHere = false;
            }
            return true;
        }
        return false;
    }

    protected virtual void Update(){
        if( startBlock() ) return;
        UpdateCameraProperties();
        HandleDirectionChange();
        move();
        TeleportByWall();
        UpdateDirectionChangeTimer();
        flipAnimation();
    }

    public virtual void OnStart(){}

    public virtual void UpdateDirectionChangeTimer(){}

}
