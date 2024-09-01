using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    string sceneName;


    public void OnClickLoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickLoadScene(string _sceneName) {
        SceneManager.LoadScene(_sceneName);
    }
}
