using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CarController
{

    private float horizontal, velocity;
    [SerializeField]
    private PathNode currentNode;
    private PathNode nodeAtLastCheckPoint;
    [SerializeField]
    private float radius = 4;
    [SerializeField]
    private float distanceToNextWaypoint;
    [SerializeField, Tooltip("number of waypoints the car's pathing will jump ahead when it reaches its current waypoint")]
    private int pathAheadDist;

 

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(horizontal);

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


        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.GetNext(pathAheadDist).transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 2;
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
            allWheels[0].motorTorque =  motorPower;
            allWheels[1].motorTorque =  motorPower;
        }
        else if (rigidBody.velocity.magnitude > 0.5)
        {
            //slow the car down if over the top speed
            allWheels[0].motorTorque = -motorPower;
            allWheels[1].motorTorque = -motorPower;
        }
    }

    public override void OnBoostPanel()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrawGizmos()
    {
        if(currentNode != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentNode.transform.position, 2);
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        currentNode = nodeAtLastCheckPoint;
    }

    public void SetCheckPointNode()
    {
       nodeAtLastCheckPoint = currentNode;
    }
}
