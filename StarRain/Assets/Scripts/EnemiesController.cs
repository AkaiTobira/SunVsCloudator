using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public List<Vector3> m_directions;
    void Awake() 
    {

       numberOfChildren = transform.childCount;

        for( int i = 0; i < numberOfChildren; i++){
            Transform current_child                                     = transform.GetChild(i);

            EnemyController EC = current_child.GetComponent<EnemyController>();
            if( EC != null){
                current_child.GetComponent<EnemyController>().index         = i;
                current_child.GetComponent<EnemyController>().numberOfChild = numberOfChildren;
            }else{
                current_child.GetComponent<EnemyRandomController>().index         = i;
                current_child.GetComponent<EnemyRandomController>().numberOfChild = numberOfChildren;
            }



            print( numberOfChildren );

            Transform next_child;
            if( i == numberOfChildren-1){
                next_child = transform.GetChild(0);
            }else{
                next_child = transform.GetChild(i+1);
            }
            m_directions.Add( (next_child.position - current_child.position).normalized );
        }
    }

    private float timer;
    private int numberOfChildren;
    public GameObject m_prefab;
    private int newChilds = 0;

    [SerializeField] private float tiemrStep = 2.0f;

    void Update()
    {
        SpawnNewChild();
    }

    void SpawnNewChild(){
        timer -= Time.deltaTime;
        if( timer < 0){
            timer += tiemrStep;
            GameObject new_child = Instantiate(m_prefab, new Vector3(-1000, -1000, 0), Quaternion.identity);
            new_child.transform.parent = this.transform;
            newChilds += 1;
            new_child.GetComponent<EnemyController>().index = Random.Range(0, numberOfChildren);
            new_child.GetComponent<EnemyController>().numberOfChild = numberOfChildren;
        }
    }


}
