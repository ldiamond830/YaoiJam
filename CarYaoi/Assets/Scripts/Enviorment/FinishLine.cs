using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    int lapCount;
    public void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponent<CarController>();
        if(car != null)
        {
            car.lapCounter++;
            if(car.lapCounter >= lapCount)
            {
                if(car is PlayerController)
                {
                    //win condition
                }
            }
        }
    }
}
