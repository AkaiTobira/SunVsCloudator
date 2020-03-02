using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : BaseController
{
    // Start is called before the first frame update

    Vector3 targetPos = new Vector3(0,0,0);
    bool movingLeft   = false;

    override protected void flipAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x < 0;
    }
    public override void OnStart(){
        UpdateCameraProperties();
        movingLeft       = ( Random.Range(0,2) == 1 );
        float x_position = ( movingLeft ) ? -screen_width*0.5f - 10.0f : screen_width*0.5f + 10.0f;
        transform.position = new Vector3(  x_position, transform.position.y, 0 );
        targetPos          = new Vector3( -x_position, Random.Range( -screen_hight*0.5f, 150 ), 0 );
    }

    override public void HandleDirectionChange(){
            m_direction = (targetPos -transform.position).normalized;
    }

    override protected void TeleportByWall(){

        if( (transform.position.x - targetPos.x) < -3.0f && !movingLeft) Destroy( gameObject );
        if( (transform.position.x - targetPos.x) >  3.0f &&  movingLeft) Destroy( gameObject );
    }

    // Update is called once per frame
}
