using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : BaseController
{
    Vector3 targetPos = new Vector3(0,0,0);
    float leftBorder  = -2000;
    float rightBorder = 2000;

    override protected void AdaptAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x > 0;
    }
    protected void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screenHight = 2f * cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    override public void Awake() {
        base.Awake();
        UpdateCameraProperties();
        bool movingLeft       = ( Random.Range(0,2) == 1 );
        float x_position = ( movingLeft ) ? leftBorder : rightBorder;
        transform.position = new Vector3(  x_position * 2, transform.position.y, 0 );
        targetPos          = new Vector3( -x_position * 2, Random.Range( -screenHight*0.5f, 150 ), 0 );
    }

    override public void ChangeDirection(){
            m_direction = (targetPos -transform.position).normalized;
    }

    override protected void TeleportByWall(){
        if( transform.position.x < leftBorder  )Destroy( gameObject );
        if( transform.position.y > rightBorder )Destroy( gameObject );
    }
}
