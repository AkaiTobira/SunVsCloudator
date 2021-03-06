﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollectableObject : MonoBehaviour
{

    [SerializeField] public float points = 50;
    [SerializeField] public int objectId;

    void OnTriggerEnter2D(Collider2D col){

        if( col.gameObject.name.Contains("Player") ) {
            Destroy(gameObject);
            AudioManager.PlayMusic("CollectedDestroyed");
        }
        if( col.gameObject.tag == "Killer" ){
            Destroy(gameObject);
            AudioManager.PlayMusic("CollectedDestroyed");
        }
        if( col.gameObject.name.Contains("FollowEnemy2")){
            Destroy(gameObject);
            AudioManager.PlayMusic("Collected");
        }

    }
}

