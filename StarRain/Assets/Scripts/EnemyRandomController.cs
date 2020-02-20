using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomController : BaseController
{
    override public void HandleDirectionChange(){
            m_direction   = transform.parent.GetComponent<EnemiesController>().m_directions[index];
    }

    override public void UpdateDirectionChangeTimer(){
        timer -= Time.deltaTime;
        if( timer < 0 ){
            index =  (index + 1) % numberOfChild;
            timer += timerStep; 
        }
    }

}

