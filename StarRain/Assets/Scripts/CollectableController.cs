

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

    private List<int> queueToSpawn = new List<int>();
    private void UpdateCameraProperties(){
        Camera cam  = Camera.main;
        screenHight = cam.orthographicSize;
        screenWidth = screenHight * cam.aspect;
    }

    void Awake() {
        UpdateCameraProperties();
        queueToSpawn.Add(3);
        queueToSpawn.Add(4);
        queueToSpawn.Add(5);
        queueToSpawn.Add(1);
        queueToSpawn.Add(0);
        queueToSpawn.Add(2);
        queueToSpawn.Add(2);
        queueToSpawn.Add(3);
        queueToSpawn.Add(4);
        queueToSpawn.Add(5);
        queueToSpawn.Add(5);
        SpawnAllBallons();
    }

    void Update()
    {
        if( ! GameState.isGameActive() ) return;
        UpdateCameraProperties();
        UpdateSpawnNewEnemy();
    }

    private void SpawnAllBallons(){
        for( int i = 0; i <  callectable.Length - 3;i++){
            GameObject new_child = Instantiate(callectable[i], 
                                                new Vector3(
                                                    Random.Range( -screenWidth, screenWidth), 
                                                    Random.Range( -screenHight, screenHight), 
                                                    0), 
                                                Quaternion.identity);
            new_child.transform.parent = this.transform;
            new_child.GetComponent<CollectableObject>().objectId = i;
        }
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
        queueToSpawn.Add(  Random.Range(0, callectable.Length) );
        int collectableArrayIndex = queueToSpawn[0];
        GameObject new_child = Instantiate(callectable[collectableArrayIndex], 
                                            new Vector3(
                                                Random.Range( -screenWidth, screenWidth), 
                                                Random.Range( -screenHight, screenHight), 
                                                0), 
                                            Quaternion.identity);
        new_child.transform.parent = this.transform;
        new_child.GetComponent<CollectableObject>().objectId = collectableArrayIndex;
        queueToSpawn.RemoveAt(0);

    }
}
