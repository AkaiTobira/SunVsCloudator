using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    public static string nextSceneName = "";
    void Start ()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene ()
    {
       yield return new WaitForSeconds(0.1f);
       SceneManager.LoadScene(nextSceneName);
    }
}
