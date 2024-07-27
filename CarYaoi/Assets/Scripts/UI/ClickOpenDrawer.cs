using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOpenDrawer : MonoBehaviour
{

    [SerializeField]
    ClickOpenDrawer otherDrawerOne, otherDrawerTwo;
    [SerializeField]
    Sprite openSprite, closedSprite;

    bool isOpen = false;

    [SerializeField]
    GameObject partOne, partTwo, partThree;

    Image image;
    Text text;

    public bool Open {
        get { return isOpen; }
    }


    public void Start() {
        image = this.GetComponent<Image>();
        text = this.GetComponent<Text>();
    }

    public void OnPointerClick()
    {

        if (isOpen) {
            CloseDrawer();
        } else {
            isOpen = true;

            otherDrawerOne.CloseDrawer();
            otherDrawerTwo.CloseDrawer();

            // -635.9
            // 790 387 open
            // 684 205 closed
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(790f, 387f);
            
            // this might be the way to adjust the positioning
            //this.GetComponent<RectTransform>().anchorMin = new Vector2(10f, 10f);
            //this.GetComponent<RectTransform>().anchorMax = new Vector2(100f, 10f);
            
            //text.gameObject.SetActive(false);

            image.sprite = openSprite;

            partOne.SetActive(true);
            partTwo.SetActive(true);
            partThree.SetActive(true);
        }
    }

    public void CloseDrawer() {
        isOpen = false;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(684f, 205f);
        image.sprite = closedSprite;
        //text.gameObject.SetActive(true);

        partOne.SetActive(false);
        partTwo.SetActive(false);
        partThree.SetActive(false);
    }
}
