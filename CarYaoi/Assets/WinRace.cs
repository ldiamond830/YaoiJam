using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinRace : MonoBehaviour
{
    [SerializeField]
    Collider player;
    [SerializeField]
    int requiredNumberOfLaps;
    [SerializeField]
    LoadScene loadScene;

    [SerializeField]
    int laps = 0;

    // Update is called once per frame
    void Update()
    {
        if (laps >= requiredNumberOfLaps) {
            loadScene.OnClickLoadScene();
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject == player) {
            laps ++;
            Debug.Log("collision!");
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col == player) {
            laps ++;
            Debug.Log("trigger");
        }
    }
}
