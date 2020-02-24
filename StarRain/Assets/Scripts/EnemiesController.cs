using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public List<Vector3> m_directions;

    private float timer;
    private int numberOfChildren;
    public GameObject[] m_prefab;

    [SerializeField] private float tiemrStep = 2.0f;
    void Awake() 
    {
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
        GameObject new_child = Instantiate(m_prefab[Random.Range(0, m_prefab.Length)], new Vector3(Random.Range( -1000, 1000), Random.Range( -1000, 1000), 0), Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<BaseController>().index = Random.Range(0, numberOfChildren);
        new_child.GetComponent<BaseController>().numberOfChild = numberOfChildren;
    }


}
