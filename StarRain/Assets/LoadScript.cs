using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    // Start is called before the first frame update

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
