using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    [SerializeField] float m_speed = 100.0f;
    protected Rigidbody2D m_rigidbody;
    protected Vector2 m_direction;

    private float screen_hight;
    private float screen_width;

    [HideInInspector] public int index;
    [HideInInspector] public int numberOfChild;

    [SerializeField] protected float timerStep = 0.2f;
    protected float timer = 0.0f;
    private void Awake() {
        m_rigidbody   = GetComponent<Rigidbody2D>();
        timer = timerStep;
    }
    private void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = 2f * cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;
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

    void Update(){
        UpdateCameraProperties();
        HandleDirectionChange();
        TeleportByWall();
        UpdateDirectionChangeTimer();
    }

    public virtual void UpdateDirectionChangeTimer(){}

}
