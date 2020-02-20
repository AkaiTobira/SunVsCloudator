using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateController : BaseController
{

    private Vector3 wheelPoint     = new Vector3(0,0,0);
    private Vector3 wheelPointNext = new Vector3(0,0,0);

    [SerializeField] public float rotatationRadius = 1.0f;
    override public void HandleDirectionChange(){
        m_direction = (wheelPointNext-wheelPoint).normalized;
    }

    override public void UpdateDirectionChangeTimer(){
        wheelPoint = wheelPointNext;

        timer += Time.deltaTime * rotatationRadius ;
        if( timer > 360.0f ) timer -= 360.0f;

        wheelPointNext = new Vector3( Mathf.Cos(timer) * 80 * 200, 
                                      Mathf.Sin(timer) * 80 * 200, 
                                      0);
    }

}
