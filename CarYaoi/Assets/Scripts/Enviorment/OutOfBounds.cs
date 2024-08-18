using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.Respawn();
        }
    }
}
