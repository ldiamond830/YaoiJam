using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField] 
    private Transform target;
    [SerializeField] 
    private float translateSpeed;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 currentTraslateVelocity = Vector3.zero;


    private void FixedUpdate()
    {
        //update position
        Vector3 targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);

        Vector3 direction = target.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
