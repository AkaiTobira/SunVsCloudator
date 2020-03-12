

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    private float timer;
    private float screenHight;

    private float screenWidth;
    public GameObject[] callectable;
    [SerializeField] private float tiemrStep = 5.0f;

    private void UpdateCameraProperties(){
        Camera cam  = Camera.main;
        screenHight = cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    void Awake() {
        UpdateCameraProperties();
    }

    void Update()
    {
        if( ! GameState.isGameActive() ) return;
        UpdateCameraProperties();
        UpdateSpawnNewEnemy();
    }

    void UpdateSpawnNewEnemy(){
        timer -= Time.deltaTime;
        if( timer < 0){
            timer += tiemrStep;
            SpawnNewCollectable();
        }
    }

    void SpawnNewCollectable(){
        UpdateCameraProperties();
        int collectableArrayIndex = Random.Range(0, callectable.Length);
        GameObject new_child = Instantiate(callectable[collectableArrayIndex], 
                                            new Vector3(
                                                Random.Range( -screenWidth, screenWidth), 
                                                Random.Range( -screenHight, screenHight), 
                                                0), 
                                            Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<CollectableObject>().objectId = collectableArrayIndex;
    }
}
