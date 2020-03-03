using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateController : BaseController
{

    private Vector3 wheelPoint     = new Vector3(0,0,0);
    private Vector3 wheelPointNext = new Vector3(0,0,0);

    [SerializeField] public float rotatationRadius = 1.0f;
    override public void HandleDirectionChange(){
        m_direction = (wheelPointNext-wheelPoint).normalized;
    }

    override protected void flipAnimation(){

    Animator anim =  GetComponent<Animator>();

    if( m_direction.x >= 0 ){
        if( m_direction.y > 0.5 ){ // RD
            anim.Play("Dino2", 0, 0.7f );
     //       print( "X,Y05");
        }else if( m_direction.y > 0.0){ // R
            anim.Play("Dino2", 0, 0.0f );
      //      print( "X,Y00");
        }else if( m_direction.y > -0.5){ // RU
            anim.Play("Dino2", 0, 0.1f );
      //      print( "X,Y-5");
        }else{
            anim.Play("Dino2", 0, 0.2f ); // U
      //      print( "X,Y-10");
        }
    }else{
        if( m_direction.y > 0.5 ){ //LU
            anim.Play("Dino2", 0, 0.3f );
      //      print( "-X,Y05");
        }else if( m_direction.y > 0.0){ //L
            anim.Play("Dino2", 0, 0.4f );
      //      print( "-X,Y00");
        }else if( m_direction.y > -0.5){ // LD
            anim.Play("Dino2", 0, 0.5f );
      //      print( "-X,Y-5");
        }else{
            anim.Play("Dino2", 0, 0.6f ); // D
      //      print( "-X,Y05");
        }
    }
    }

    override public void UpdateDirectionChangeTimer(){
        wheelPoint = wheelPointNext;

        timer += Time.deltaTime * rotatationRadius ;
        if( timer > 360.0f ) timer -= 360.0f;

        wheelPointNext = new Vector3( Mathf.Cos(timer) * 80 * 200, 
                                      Mathf.Sin(timer) * 80 * 200, 
                                      0);
    }

}
