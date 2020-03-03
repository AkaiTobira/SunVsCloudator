using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{

    override protected void flipAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x < 0;
    }
    override public void HandleDirectionChange(){
           // m_direction = transform.parent.GetComponent<EnemiesController>().m_directions[index];
    }
}

