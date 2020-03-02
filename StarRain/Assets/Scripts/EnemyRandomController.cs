using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomController : BaseController
{
    private Vector2 targetPosition = new Vector3( 0,0 );
    private const float direction_change_speed = 0.05f;


    override public void OnStart(){
        timerStep = 3.0f;
    }
    override public void HandleDirectionChange(){
            Vector2 converted_pos_vector3 = new Vector2( transform.position.x, transform.position.y);
            Vector2 target_direction = (targetPosition - converted_pos_vector3).normalized;
            m_direction  += (target_direction - m_direction) * direction_change_speed;
            m_direction  = m_direction.normalized;
    }

    override public void UpdateDirectionChangeTimer(){
        timer -= Time.deltaTime;
        if( timer < 0 ){
            targetPosition = new Vector3( Random.Range( -screen_width, screen_width), Random.Range( -screen_hight, screen_hight));
            timer += timerStep; 
        }
    }

}

