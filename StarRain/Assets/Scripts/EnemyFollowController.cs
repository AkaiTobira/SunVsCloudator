using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFollowController : BaseController
{

    [SerializeField] private GameObject playerNode = null;

    override protected void flipAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x < 0;
    }

    override public void HandleDirectionChange(){
        if( playerNode == null ) return;
        m_direction = (playerNode.transform.position - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.name == "Player") return;
        Destroy(col.gameObject);
    }

}

