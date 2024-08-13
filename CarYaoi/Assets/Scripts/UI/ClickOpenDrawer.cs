using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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

    // Part connections
    // If you have any questions, ask Brian
    [SerializeField]
    int partTier;
    //Public mainly just for debugging purposes
    public List<PartScriptableObject> partData;

    public bool Open {
        get { return isOpen; }
    }


    public void Start() {
        image = this.GetComponent<Image>();
        text = this.GetComponent<Text>();

        
        partData = MechanicScript.Instance.GetPartsOfTier(partTier);
        // This could probably be optimized, not too familiar with unity
        // Putting the info from the scriptable object into the UI
        // Not sure how to do user inputs, but when clicked add the scriptable object to the player's inventory if allowed
        void SetParts(GameObject part, int dataIndex)
        {
            PartScriptableObject data = partData[dataIndex];
            //This doesn't seem to be the image that's presented
            GameObject image = part.transform.GetChild(0).gameObject;
            image.GetComponent<Image>().sprite = data.image;

            Transform button = part.transform.GetChild(1);
            //button.GetComponent<Image>().sprite = data.image;
            Transform name = button.GetChild(0);
            name.GetComponent<TextMeshProUGUI>().text = data.name;
            Transform slots = button.GetChild(1);
            slots.GetComponent<TextMeshProUGUI>().text = "Slots: " + data.stats.pointCost.ToString();

            Transform statBlock = button.GetChild(2);
            statBlock.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SPD: " + data.stats.topSpeed;
            statBlock.GetChild(1).GetComponent<TextMeshProUGUI>().text = "ACC: " + data.stats.acceleration;
            statBlock.GetChild(2).GetComponent<TextMeshProUGUI>().text = "MNV: " + data.stats.turnSpeed;
            statBlock.GetChild(3).GetComponent<TextMeshProUGUI>().text = "BST: " + data.stats.boosts;

        }
        if (partData.Count < 1) { return; }
        SetParts(partOne, 0);
        if (partData.Count < 2) { return; }
        SetParts(partTwo, 1);
        if (partData.Count < 3) { return; }
        SetParts(partThree, 2);

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
