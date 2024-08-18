// Moss Limpert 7/24/24
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://discussions.unity.com/t/how-to-detect-if-mouse-is-over-ui/821330
public class OnHoverButton : MonoBehaviour
{
    int UILayer;
    [SerializeField]
    GameObject hovered;

    // Start is called before the first frame update
    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPointerOverUIElement())
        {
            hovered.SetActive(true);
        } else
        {
            hovered.SetActive(false);
        }
        
    }

    // returns true if we touched/are hovering on the UI element
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int index = 0; index < eventSystemRaycastResults.Count; index++)
        {
            RaycastResult curRaycastResult = eventSystemRaycastResults[index];
            if (curRaycastResult.gameObject.layer == UILayer && curRaycastResult.gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}
