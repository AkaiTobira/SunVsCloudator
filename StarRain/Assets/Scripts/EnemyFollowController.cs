using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFollowController : BaseController
{
    override public void HandleDirectionChange(){
        m_direction = (transform.parent.Find("Player").position - transform.position).normalized;
    }

}

