using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableGameObject : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;
    

    

    public void ToggleEnable(bool enable)
    {
        gameObject.SetActive(enable);
    }
}
