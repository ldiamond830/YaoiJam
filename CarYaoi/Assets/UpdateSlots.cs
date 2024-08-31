using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateSlots : MonoBehaviour
{

    [SerializeField]
    MechanicScript mechanicSingleton;
    [SerializeField]
    TMP_Text slotsText;

    int curSlots;

    // Start is called before the first frame update
    void Start()
    {
        // get number of slots
        curSlots = mechanicSingleton.Slots();
        // put it in the text
        slotsText.text = "SLOTS: " + curSlots;
    }

    // Update is called once per frame
    void Update()
    {
        curSlots = mechanicSingleton.Slots();
    }

    // called at the end. used for ui
    private void LateUpdate()
    {
        slotsText.text = "SLOTS: " + curSlots;
    }
}
