using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    private float timer;
    private float screenHight;

    private bool is_cop_apeared = false;

    private float screenWidth;
    public GameObject[] m_prefab;

    [SerializeField] private GameObject followerEnemy;

    [SerializeField] private float tiemrStep      = 2.0f;
    [SerializeField] private float timerReduction = 0.1f;
    [SerializeField] private float minTimeStep    = 1.0f;
    private void UpdateCameraProperties(){
        Camera cam  = Camera.main;
        screenHight = cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    void Awake() {
        StopAllEnemies();
    }

    void StopAllEnemies(){
        for( int i = 0; i < transform.childCount; i ++){
            Transform child = transform.GetChild(i);
            child.GetComponent<Renderer>().enabled       = false;
            child.GetComponent<Animator>().enabled       = false;
            child.GetComponent<BaseController>().enabled = false;
        }
    }

    public void RunAllEnemies(){
        for( int i = 0; i < transform.childCount; i ++){
            Transform child = transform.GetChild(i);
            child.GetComponent<Renderer>().enabled       = true;
            child.GetComponent<Animator>().enabled       = true;
            child.GetComponent<BaseController>().enabled = true;
        }
    }

    public Vector3 GetVectorPosition(){
        Vector3 enemyPosition =  new Vector3( Random.Range( -screenWidth, screenWidth), 
                                              Random.Range( -screenHight, screenWidth), 0);
        while ( Vector3.Distance( transform.parent.GetChild(3).transform.position, enemyPosition ) < 100 ){
            enemyPosition =  new Vector3( Random.Range( -screenWidth, screenWidth), 
                                          Random.Range( -screenHight, screenWidth), 0);
        }

        return enemyPosition;
    }

    void SpawnCop(){
        if( transform.parent.GetComponent<GameController>().timer < 15 ) return;
        UpdateCameraProperties();
        is_cop_apeared = true;
        GameObject new_child = Instantiate(followerEnemy, 
                                           GetVectorPosition(),
                                           Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<EnemyFollowController>().playerNode = transform.parent.GetChild(3).gameObject;
        new_child.GetComponent<BaseController>().SetScreenSize( screenHight, screenWidth);
        new_child.GetComponent<BaseController>().Awake();
    }

    void Update(){
        if( ! GameState.isGameActive() ) return;
        UpdateCameraProperties();
        UpdateSpawnNewEnemy();
        if( is_cop_apeared ) return;
        SpawnCop();
    }

    void UpdateSpawnNewEnemy(){
        timer -= Time.deltaTime;
        if( timer < 0){
            timer += tiemrStep;
            SpawnNewEnemy();
            tiemrStep = Mathf.Max( tiemrStep-timerReduction, minTimeStep );
        }
    }

    void SpawnNewEnemy(){
        UpdateCameraProperties();
        GameObject new_child = Instantiate( m_prefab[Random.Range(0, m_prefab.Length)], 
                                            GetVectorPosition(), 
                                            Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<BaseController>().SetScreenSize( screenHight, screenWidth);
        new_child.GetComponent<BaseController>().Awake();
    }
}
