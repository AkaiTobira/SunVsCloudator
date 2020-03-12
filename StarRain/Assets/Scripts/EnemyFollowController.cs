using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFollowController : BaseController
{

    [SerializeField] public GameObject playerNode = null;

    override public void Awake(){
        base.Awake();
        GetComponent<Renderer>().sortingOrder  = 2001;
        sortY = false;
    }

    override protected void AdaptAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x > 0;
    }

    override public void ChangeDirection(){
        if( playerNode == null ) return;
        m_direction = (playerNode.transform.position - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.name.Contains("Player") ) return;
        col.gameObject.GetComponent<Animator>().SetBool("isAlive", false);
        Destroy (col.gameObject, col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); //GetCurrentAnimatorStateInfo(0).length ); 
    //    Destroy(col.gameObject);
    }

}

