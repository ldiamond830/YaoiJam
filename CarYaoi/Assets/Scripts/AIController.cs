using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CarController
{

    private float horizontal, velocity;
    [SerializeField]
    private PathNode currentNode;
    [SerializeField]
    private float radius = 4;
    [SerializeField]
    private float distanceToNextWaypoint;


    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(horizontal);

        AccelerateCar();
        SteerCar();

        for (int i = 0; i < allWheels.Length; i++)
        {
            //allWheels[i].brakeTorque = breakToggle * breakPower;

            UpdateSingleWheel(allWheels[i], allTransforms[i]);
        }
    }

    protected override void SteerCar()
    {
        if(Vector3.Distance(transform.position, currentNode.transform.position) <= distanceToNextWaypoint) {
            currentNode = currentNode.next;
        }


        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 3;
        horizontal = Mathf.SmoothDamp(horizontal, newSteer, ref velocity, Time.deltaTime * 2);
        if (horizontal > 0)
        {
            allWheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            allWheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            allWheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            allWheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        }
        else
        {
            allWheels[0].steerAngle = 0;
            allWheels[1].steerAngle = 0;
        }
    }

    protected override void AccelerateCar()
    {
        if (rigidBody.velocity.magnitude < topSpeed)
        {
            frontLeftWheelCol.motorTorque =  motorPower;
            frontRightWheelCol.motorTorque =  motorPower;
        }
        else if (rigidBody.velocity.magnitude > 0.5)
        {
            //slow the car down if over the top speed
            frontLeftWheelCol.motorTorque = -motorPower;
            frontRightWheelCol.motorTorque = -motorPower;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentNode.transform.position, 2);
    }
}
