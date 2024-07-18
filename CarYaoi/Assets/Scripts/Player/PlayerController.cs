using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float motorPower;
    [SerializeField]
    private float breakPower;
    [SerializeField] 
    private float maxSteeringAngle;
    [SerializeField]
    private float topSpeed;
    [SerializeField]
    private int boostCharges;
    [SerializeField] 
    private float boostDuration;
    private bool isBoosting;
    private float boostTimer;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private WheelCollider frontLeftWheelCol;
    [SerializeField]
    private WheelCollider frontRightWheelCol;
    [SerializeField]
    private WheelCollider rearLeftWheelCol;
    [SerializeField]
    private WheelCollider rearRightWheelCol;

    [SerializeField]
    private Transform frontLeftWheelTransform;
    [SerializeField]
    private Transform frontRightWheelTransform;
    [SerializeField]
    private Transform rearLeftWheelTransform;
    [SerializeField]
    private Transform rearRightWheelTransform;

    private WheelCollider[] allWheels = new WheelCollider[4];
    private Transform[] allTransforms = new Transform[4];

    private Vector2 Direction;
    private float breakToggle = 0.0f;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    public void OnBoostInput(InputAction.CallbackContext context)
    {
        if(boostCharges > 0 && !isBoosting)
        {
            boostCharges--;
            isBoosting = true;
            boostTimer = boostDuration;
            topSpeed += 5;
            motorPower *= 2;
        }
    }

    //toggles break on when button is held and off when it is released
    public void OnBreakInput(InputAction.CallbackContext context)
    {
       breakToggle = context.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        allWheels[0] = frontLeftWheelCol;
        allWheels[1] = frontRightWheelCol;
        allWheels[2] = frontLeftWheelCol;
        allWheels[3] = frontRightWheelCol;

        allTransforms[0] = frontLeftWheelTransform;
        allTransforms[1]    = frontRightWheelTransform;
        allTransforms[2] = frontLeftWheelTransform;
        allTransforms[3] = frontRightWheelTransform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rigidBody.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;

            if(boostTimer <= 0)
            {
                topSpeed -= 5;
                motorPower /= 2;
                isBoosting = false;

            }
        }

        if(rigidBody.velocity.magnitude < topSpeed)
        {
            frontLeftWheelCol.motorTorque = Direction.y * motorPower;
            frontRightWheelCol.motorTorque = Direction.y * motorPower;
        }
        else
        {
            //slow the car down if over the top speed
            frontLeftWheelCol.motorTorque = Direction.y * motorPower * -1;
            frontRightWheelCol.motorTorque = Direction.y * motorPower * -1;
        }

        float steerAngle = maxSteeringAngle * Direction.x;
        frontLeftWheelCol.steerAngle = steerAngle;
        frontRightWheelCol.steerAngle = steerAngle;

        
        for(int i = 0; i < allWheels.Length; i++)
        {
            allWheels[i].brakeTorque = breakToggle * breakPower;

            UpdateSingleWheel(allWheels[i], allTransforms[i]);
        }
        
    }

    private void UpdateSingleWheel(WheelCollider wheel, Transform transform)
    {
        Vector3 newPos;
        Quaternion newRot;
        wheel.GetWorldPose(out newPos, out newRot);
        transform.position = newPos;
        transform.rotation = newRot;
    }
}
