using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSprite : MonoBehaviour
{
    [SerializeField]
    GameObject mechanic, climaxx, acura;

    // Start is called before the first frame update
    void Start()
    {

    }

    // show Maxx's sprite
    public void showMaxx()
    {
        climaxx.SetActive(true);
    }

    // hide Maxx's sprite
    public void hideMaxx()
    {
        climaxx.SetActive(false);
    }

    // show Acura's sprite
    public void showAcura()
    {
        acura.SetActive(true);
    }

    // hide Acura's sprite
    public void hideAcura()
    {
        acura.SetActive(false);
    }

    // show Mechanic's sprite
    public void showMechanic()
    {
        mechanic.SetActive(true);
    }

    // hide Mechanic's sprite
    public void hideMechanic()
    {
        mechanic.SetActive(false);
    }
}
