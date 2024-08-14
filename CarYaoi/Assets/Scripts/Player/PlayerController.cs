using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    private float nitroDuration;
    private bool nitroActive;
    private float nitroTimer;
    private bool isBoosted;

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

    [SerializeField]
    TextMeshProUGUI SpeedText;

    private WheelCollider[] allWheels = new WheelCollider[4];
    private Transform[] allTransforms = new Transform[4];

    private Vector2 Direction;
    private float breakToggle = 0.0f;
    private float accelToggle = 0.0f;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    public void OnNitroInput(InputAction.CallbackContext context)
    {
        if(boostCharges > 0 && !nitroActive)
        {
            boostCharges--;
            nitroActive = true;
            nitroTimer = nitroDuration;
            topSpeed += 5;
            motorPower *= 2;
        }
    }

    //toggles break on when button is held and off when it is released
    public void OnBreakInput(InputAction.CallbackContext context)
    {
       breakToggle = context.ReadValue<float>();
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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(accelToggle);
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

        if (accelToggle != 0)
        {
            if (Mathf.Abs(rigidBody.velocity.magnitude) < topSpeed)
            {
                frontLeftWheelCol.motorTorque = Direction.y * motorPower;
                frontRightWheelCol.motorTorque = Direction.y * motorPower;
            }
            else //if (Mathf.Abs(rigidBody.velocity.magnitude) > 0.5)
            {
                //slow the car down if over the top speed
                frontLeftWheelCol.motorTorque = Direction.y * motorPower * -1;
                frontRightWheelCol.motorTorque = Direction.y * motorPower * -1;
            }

            if (breakToggle != 0)
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = breakToggle * breakPower;
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
            if (breakToggle != 0)
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = breakPower;
                }
            }
            //if not holding the accelerator or break slow down less than holding the break
            else 
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].brakeTorque = (breakPower / 1.3f);
                }
            }
        }

        float steerAngle = maxSteeringAngle * Direction.x;
        frontLeftWheelCol.steerAngle = steerAngle;
        frontRightWheelCol.steerAngle = steerAngle;

        
        for(int i = 0; i < allWheels.Length; i++)
        {

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

    public void OnBoostPanel()
    {
        if(!isBoosted)
        {
            isBoosted = true;
            topSpeed += 2;
            frontLeftWheelCol.motorTorque = Direction.y * (motorPower * 20);
            frontRightWheelCol.motorTorque = Direction.y * (motorPower * 20);
            Debug.Log("Boostio");
        }
    }
}
