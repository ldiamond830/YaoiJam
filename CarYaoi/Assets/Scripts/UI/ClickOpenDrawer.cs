using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOpenDrawer : MonoBehaviour, IPointerClickHandler
{
    GameObject openDrawer;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (openDrawer.activeSelf)
    }
}
