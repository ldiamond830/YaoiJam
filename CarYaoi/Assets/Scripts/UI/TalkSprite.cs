using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSprite : MonoBehaviour
{
    [SerializeField]
    [Tooltip("You dont have to assign the mechanic if he's not in the scene")]
    GameObject mechanic_neutral,mechanic_teehee, climaxx_neutral, climaxx_annoyed, climaxx_smug, acura_neutral, acura_happy;

    // Start is called before the first frame update
    void Start()
    {

    }

//
// MAXX
//
    // show Maxx's Neutral sprite
    public void showMaxxNeutral()
    {
        climaxx_neutral.SetActive(true);
    }

    // hide Maxx's Neutral sprite
    public void hideMaxxNeutral()
    {
        climaxx_neutral.SetActive(false);
    }

    // show Maxx's Annoyed sprite
    public void showMaxxAnnoyed()
    {
        climaxx_annoyed.SetActive(true);
    }

    // hide maxx's annoyed sprite
    public void hideMaxxAnnoyed()
    {
        climaxx_annoyed.SetActive(false);
    }

    // show Maxx's smug sprite
    public void showMaxxSmug() {
        climaxx_smug.SetActive(true);
    }

    // hide Maxx's smug sprite
    public void hideMaxxSmug(){
        climaxx_smug.SetActive(false);
    }

    //
    // ACURA
    //

    // show Acura's sprite
    public void showAcuraNeutral()
    {
        acura_neutral.SetActive(true);
    }

    // hide Acura's sprite
    public void hideAcuraNeutral()
    {
        acura_neutral.SetActive(false);
    }

    // show Acura happy sprite
    public void showAcuraHappy()
    {
        acura_happy.SetActive(true);
    }

    // hide acura happy sprite
    public void hideAcuraHappy() {
        acura_happy.SetActive(false);
    }


    //
    // MECHANIC
    //

    // show Mechanic's sprite
    public void showMechanicNeutral()
    {
        mechanic_neutral.SetActive(true);
    }

    // hide Mechanic's sprite
    public void hideMechanicNeutral()
    {
        mechanic_neutral.SetActive(false);
    }

    // show mechanic tee hee sprite
    public void showMechanicTeehee() {
        mechanic_teehee.SetActive(true);
    }

    // hide mechanic tee hee sprite
    public void hideMechanicTeehee() {
        mechanic_teehee.SetActive(false);
    }

}
