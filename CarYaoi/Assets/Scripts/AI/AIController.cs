using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField, Tooltip("number of waypoints the car's pathing will jump when reversing to get unstuck")]
    private int pathBehindDist;
    private Vector3 prevPosition;
    private bool isReversing = false;
    private float stuckTimer = 1;
    [SerializeField]
    private float reverseThreshold;
    [SerializeField]
    private float reverseTime;
    [SerializeField]
    private float checkStuckTime;

    public override void Start()
    {
        base.Start();   
        prevPosition = transform.position;
    }

    private void Update()
    {
        Debug.Log(motorPower);
        //check if the car has gotten stuck
       
            stuckTimer -= Time.deltaTime;

            if (stuckTimer < 0)
            {
                stuckTimer = checkStuckTime;
                if(!isReversing)
                {
                    //if the distance between the current position and position one second ago is within a certain threshold start reversing to the previous waypoint
                    if (Vector3.Distance(transform.position, prevPosition) < reverseThreshold)
                    {
                        isReversing = true;
                        motorPower *= -1;
                        stuckTimer = reverseTime;
                }

                }
                else
                {
                    isReversing = false;
                    motorPower *= -1; 
                }
                prevPosition = transform.position;
            }
        
    }

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

        Vector3 relativeVector;
        if (!isReversing)
        {
            relativeVector = transform.InverseTransformPoint(currentNode.GetNext(pathAheadDist).transform.position);
        }
        else
        {
            relativeVector = transform.InverseTransformPoint(currentNode.GetPrev(pathBehindDist).transform.position);
        }       
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 2;
        horizontal = Mathf.SmoothDamp(horizontal, newSteer, ref velocity, Time.deltaTime * 2);
        
        if (isReversing)
        {
            horizontal *= -1;
        }
        
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
