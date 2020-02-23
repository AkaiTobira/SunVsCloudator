using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    float timer = 0;
    [SerializeField] const float pointsMultiplyer = 5;
    [SerializeField] public GameObject UiText;
    [SerializeField] public GameObject UiTextasgg;
    void Awake() {
        Camera.main.aspect = 1.77f;
    }

void Update()
{
    timer += Time.deltaTime;
    UiText.GetComponent<Text>().text = "Points : " + ((int)(timer * pointsMultiplyer)).ToString();
   // UiTextasgg.GetComponent<Text>().text = transform.GetChild(2).GetComponent<PlayerController>().mousePosition.ToString(); 
 
}

}
