using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    private float timer;
    private float screen_hight;

    private float screen_width;
    public GameObject[] m_prefab;

    [SerializeField] private float tiemrStep = 2.0f;
    private void UpdateCameraProperties(){
        Camera cam = Camera.main;
        screen_hight = cam.orthographicSize;
        screen_width = screen_hight * cam.aspect;
    }

    void Update()
    {
        UpdateCameraProperties();
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
        GameObject new_child = Instantiate(m_prefab[Random.Range(0, m_prefab.Length)], new Vector3(Random.Range( -screen_width, screen_width), Random.Range( -screen_hight, screen_hight), 0), Quaternion.identity);
        new_child.transform.parent = this.transform;
    }


}
