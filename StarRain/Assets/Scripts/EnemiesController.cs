using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public List<Vector3> m_directions;

    private float timer;
    private int numberOfChildren;
    public GameObject m_prefab;
    private int newChilds = 0;

    [SerializeField] private float tiemrStep = 2.0f;
    void Awake() 
    {

       numberOfChildren = transform.childCount;

        for( int i = 0; i < numberOfChildren; i++){
            Transform current_child = transform.GetChild(i);
            Transform next_child    = GetNextEnemy(i);
            SetEnemyIndexRelatedValues( i );
            m_directions.Add( (next_child.position - current_child.position).normalized );
        }
    }

    private Transform GetNextEnemy( int current_child_index ){
        return ( current_child_index == numberOfChildren -1) ? transform.GetChild(0) : transform.GetChild(current_child_index+1);
    }

    private void SetEnemyIndexRelatedValues( int index ){
            Transform current_child = transform.GetChild(index);
            BaseController EC = current_child.GetComponent<BaseController>();
            EC.index          = index;
            EC.numberOfChild  = numberOfChildren;
    }

    void Update()
    {
        UpdateTimerToSpawnNewChild();
    }

    void UpdateTimerToSpawnNewChild(){
        timer -= Time.deltaTime;
        if( timer < 0){
            timer += tiemrStep;
            SpawnNewEnemy();
        }
    }

    void SpawnNewEnemy(){
        GameObject new_child = Instantiate(m_prefab, new Vector3(-1000, -1000, 0), Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<BaseController>().index = Random.Range(0, numberOfChildren);
        new_child.GetComponent<BaseController>().numberOfChild = numberOfChildren;
    }


}
