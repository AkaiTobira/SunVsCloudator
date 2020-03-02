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

    Animator anim =  GetComponent<Animator>();//.Play("Dino2", 0, timerStep);

    Vector3 stat = new Vector3( 1, 0, 0);

    print( Vector3.Angle( stat, m_direction ));
    float angel =  (Vector3.Angle( stat, m_direction ));

    if( angel >   0 && angel <  45) anim.Play("Dino2", 0, 0.0f );
    if( angel >  45 && angel <  90) anim.Play("Dino2", 0, 0.1f );
    if( angel >  90 && angel < 135) anim.Play("Dino2", 0, 0.2f );
    if( angel > 135 && angel < 180) anim.Play("Dino2", 0, 0.3f );
    if( angel > 180 && angel < 225) anim.Play("Dino2", 0, 0.4f );
    if( angel > 225 && angel < 270) anim.Play("Dino2", 0, 0.5f );
    if( angel > 270 && angel < 315) anim.Play("Dino2", 0, 0.6f );
    if( angel > 315 && angel < 360) anim.Play("Dino2", 0, 0.7f );

    //anim.Play("Dino2", 0, (int)(Vector3.Angle( stat, m_direction ) + 15)/45.0f );


  //  print( is_left.ToString() + is_right.ToString() + is_up.ToString() + is_down.ToString());




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
