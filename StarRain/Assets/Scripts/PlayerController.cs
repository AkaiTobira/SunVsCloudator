using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_speed = 250f;
    private Rigidbody2D m_rigidbody;
    public Vector3 mousePosition; 
    private Vector2 m_direction;

    private float screen_hight;
    private float screen_width;

    private void Awake() {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = 2f * cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;

//        print( screen_hight.ToString() + " : " + screen_width.ToString());
    }

    private void HandleInput(){

        foreach( Touch t in Input.touches ){
    //        if( t.phase == TouchPhase.Began ){
                mousePosition = Camera.main.ScreenToWorldPoint(t.position);
      //      }
        }

        if ( Input.GetMouseButton(0) ){
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        m_direction   = (mousePosition - transform.position).normalized;
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
    }

     void OnCollisionEnter(Collision col){
     //    print(col.gameObject.name);
     }

}
