using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFollowController : BaseController
{

    [SerializeField] public GameObject playerNode = null;
    private bool isDead = false;
    private float savedSpeed = 0;

    override public void Awake(){
        base.Awake();
        GetComponent<Renderer>().sortingOrder  = 2001;
        sortY = false;
    }

    override protected void AdaptAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x > 0;
    }

    protected override void Update(){
        if(isDead) base.m_rigidbody.velocity = new Vector2(0,0);
        else base.Update();
    }

    override public void ChangeDirection(){
        if( playerNode == null ) return;
        m_direction = (playerNode.transform.position - transform.position).normalized;
    }

    IEnumerator ResetFireTrigger ()
    {
       yield return new WaitForSeconds(4.5f);
       GetComponent<Animator>().ResetTrigger("isDead");
       isDead = false;

    }

    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.name.Contains("Player") ) return;
        if( col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<Animator>().SetBool("isAlive", false);
            Destroy (col.gameObject, col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        if( col.gameObject.tag == "Killer"){
            GetComponent<Animator>().SetTrigger("isDead");
            isDead      = true;
            StartCoroutine(ResetFireTrigger());
        }
        
    }

}

