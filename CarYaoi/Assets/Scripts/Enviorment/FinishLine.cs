using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    int lapCount;

    [SerializeField]
    LoadScene loadScene;

    [SerializeField]
    TrackNumberCounter trackNumberCounter;

    [SerializeField]
    int trackNumber;

    public void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponent<CarController>();
        if(car != null)
        {
            car.lapCounter++;
            Debug.Log(car.lapCounter);
            if(car.lapCounter >= lapCount)
            {
                if(car is PlayerController)
                {
                    //win 
                    if (trackNumber == 1)
                    {
                        loadScene.OnClickLoadScene("VN_AfterRace_1");
                    } else if (trackNumber == 2)
                    {
                        loadScene.OnClickLoadScene("VN_AfterRace_2");
                    } else loadScene.OnClickLoadScene("VN_Win");
                }
                else
                {
                    //loss
                    if (trackNumber == 1)
                    {
                        loadScene.OnClickLoadScene("VN_AfterRace_1");
                    } else if (trackNumber == 2)
                    {
                        loadScene.OnClickLoadScene("VN_AfterRace_2");
                    } else loadScene.OnClickLoadScene("VN_Win");

                }
            }
        }
    }
}
