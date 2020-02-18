using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    float timer = 0;
    [SerializeField] public GameObject UiText;
    [SerializeField] public GameObject UiTextasgg;
    void Awake() {
    
    }

void Update()
{
    timer += Time.deltaTime;
    UiText.GetComponent<Text>().text = "Time : " + timer.ToString();
    UiTextasgg.GetComponent<Text>().text = transform.GetChild(2).GetComponent<PlayerController>().mousePosition.ToString(); 
    
    
    timer.ToString();
}

}
