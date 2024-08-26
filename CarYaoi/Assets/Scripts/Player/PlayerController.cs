using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : CarController
{
    [SerializeField]
    private Transform CenterOfMass;
    [SerializeField]
    private int nitroCharges;
    [SerializeField] 
    private float nitroDuration;
    private bool nitroActive;
    private float nitroTimer;

    [SerializeField]
    private CameraFollow cam;
    
    [SerializeField]
    TextMeshProUGUI SpeedText;

    private Vector2 Direction;
    private float brakeToggle = 0.0f;

    private List<PartScriptableObject> parts;
    private Stats stats;
    private float accelToggle = 0.0f;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    public void OnNitroInput(InputAction.CallbackContext context)
    {
        if(nitroCharges > 0 && !nitroActive)
        {
            nitroCharges--;
            nitroActive = true;
            nitroTimer = nitroDuration;
            topSpeed += 5;
            motorPower *= 2;
        }
    }

    //toggles break on when button is held and off when it is released
    public void OnBrakeInput(InputAction.CallbackContext context)
    {
       brakeToggle = context.ReadValue<float>();
    }

    public void OnGasInput(InputAction.CallbackContext context)
    {
        accelToggle = context.ReadValue<float>();
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

        if(MechanicScript.Instance != null) 
        { 
            parts = MechanicScript.Instance.playerInventory;
            stats = MechanicScript.Instance.playerStats;

            motorPower += stats.acceleration;
            brakePower += stats.braking;
            topSpeed += stats.topSpeed;
            maxSteeringAngle += stats.turnSpeed;
            nitroCharges += stats.boosts;
        }

        rigidBody.centerOfMass = CenterOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedText.text = "Speed: " + (int)rigidBody.velocity.magnitude;
    }

    void FixedUpdate()
    {
        if (nitroActive)
        {
            nitroTimer -= Time.deltaTime;

            if(nitroTimer <= 0)
            {
                topSpeed -= 5;
                motorPower /= 2;
                nitroActive = false;

            }
        }

        if (isBoosted)
        {
            boostDuration -= Time.deltaTime;

            if(boostDuration <= 0)
            {
                topSpeed -= 2;
                isBoosted = false;
            }
        }

        AccelerateCar();

        SteerCar();

        
        for(int i = 0; i < allWheels.Length; i++)
        {

            UpdateSingleWheel(allWheels[i], allTransforms[i]);
        }
        
    }

    protected override void AccelerateCar()
    {
        if (accelToggle != 0)
        {
            if (Mathf.Abs(rigidBody.velocity.magnitude) < topSpeed)
            {
                allWheels[0].motorTorque = Direction.y * motorPower;
                allWheels[1].motorTorque = Direction.y * motorPower;
            }
            else
            {
                //slow the car down if over the top speed
                allWheels[0].motorTorque = Direction.y * motorPower * -1;
                allWheels[1].motorTorque = Direction.y * motorPower * -1;
            }

            if (brakeToggle != 0)
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = brakeToggle * brakePower;
                }
            }
            else
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = 0;
                }
            }
        }
        else
        {
            if (brakeToggle != 0)
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = brakePower;
                }
            }
            //if not holding the accelerator or break slow down less than holding the break
            else
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = (brakePower / 1.3f);
                }
            }
        }
    }

    protected override void SteerCar()
    {
        float steerAngle = maxSteeringAngle * Direction.x;
        frontLeftWheelCol.steerAngle = steerAngle;
        frontRightWheelCol.steerAngle = steerAngle;
    }

    public override void OnBoostPanel()
    {
        if(!isBoosted)
        {
            isBoosted = true;
            topSpeed += 2;
            //frontLeftWheelCol.motorTorque = Direction.y * (motorPower * 20);
            //frontRightWheelCol.motorTorque = Direction.y * (motorPower * 20);
            rigidBody.velocity = rigidBody.velocity.normalized * topSpeed;
            boostTimer = boostDuration;
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        cam.Reset();
    }
}
