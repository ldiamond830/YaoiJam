using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBackgroundObjectsDuringDialogue : MonoBehaviour
{
    [SerializeField]
    List<GameObject> backgroundObjects;

    // disable background objects
    public void disableBGObjects ()
    {
        for (int i = backgroundObjects.Count; i > 0; i--)
        {
            backgroundObjects[i].SetActive(false);
        }
    }

    // enable background objects
    public void enableBGObjects()
    {
        for (int i = backgroundObjects.Count - 1; i > 0; i--)
        {
            backgroundObjects[i].SetActive(true);
        }
    }
}
