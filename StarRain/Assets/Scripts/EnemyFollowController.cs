using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFollowController : BaseController
{
    override public void HandleDirectionChange(){
        m_direction = (transform.parent.Find("Player").position - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.name == "Player") return;
        Destroy(col.gameObject);
    }

}

