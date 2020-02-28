using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    [SerializeField] float m_speed = 100.0f;
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
    private void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = 2f * cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;
    }

    private void flipAnimation(){
            GetComponent<SpriteRenderer>().flipX = m_direction.x > 0;
    }
    public virtual void HandleDirectionChange(){
            m_direction  = new Vector3(1,0,0);
    }
    private void TeleportByWall(){
        m_rigidbody.velocity = new Vector2( m_direction.x * m_speed, m_direction.y * m_speed );
        if( transform.position.x < -screen_width*0.5f ){ transform.position = new Vector3(  screen_width*0.5f, transform.position.y ); }
        if( transform.position.x >  screen_width*0.5f ){ transform.position = new Vector3( -screen_width*0.5f, transform.position.y ); }
        if( transform.position.y < -screen_hight*0.5f ){ transform.position = new Vector3( transform.position.x,  screen_hight*0.5f ); }
        if( transform.position.y >  screen_hight*0.5f ){ transform.position = new Vector3( transform.position.x, -screen_hight*0.5f ); }
    }

    protected virtual void Update(){
        if( BlockHere ) {
            timer += Time.deltaTime;
            if(timer > 1.0){
                timer = 0;
                BlockHere = false;
            }
            return;
        }
        UpdateCameraProperties();
        HandleDirectionChange();
        TeleportByWall();
        UpdateDirectionChangeTimer();
        flipAnimation();
    }

    public virtual void UpdateDirectionChangeTimer(){}

}
