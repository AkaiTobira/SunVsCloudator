using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    override public void HandleDirectionChange(){
            m_direction = transform.parent.GetComponent<EnemiesController>().m_directions[index];
    }
}

