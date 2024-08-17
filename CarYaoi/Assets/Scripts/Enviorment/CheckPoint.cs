using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponent<CarController>();
        if(car != null)
        {
            car.checkPoint = respawnPoint;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(respawnPoint.position, respawnPoint.position + respawnPoint.forward);
    }
}
