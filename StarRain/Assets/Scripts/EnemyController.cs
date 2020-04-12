using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    override protected void AdaptAnimation(){
        GetComponent<SpriteRenderer>().flipX = m_direction.x < 0;
    }
}

