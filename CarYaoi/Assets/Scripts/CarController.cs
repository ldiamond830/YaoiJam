using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveType
{
    frontWheelDrive,
    rearWheelDrive
}

public abstract class CarController : MonoBehaviour
{
    [SerializeField]
    protected Transform CenterOfMass;
    [SerializeField]
    private DriveType driveType = DriveType.frontWheelDrive;

    [SerializeField]
    protected float motorPower;
    [SerializeField]
    protected float brakePower;
    [SerializeField]
    protected float maxSteeringAngle;
    [SerializeField]
    protected float topSpeed;

    [SerializeField]
    protected Rigidbody rigidBody;

    [SerializeField]
    protected WheelCollider frontLeftWheelCol;
    [SerializeField]
    protected WheelCollider frontRightWheelCol;
    [SerializeField]
    protected WheelCollider rearLeftWheelCol;
    [SerializeField]
    protected WheelCollider rearRightWheelCol;

    [SerializeField]
    protected Transform frontLeftWheelTransform;
    [SerializeField]
    protected Transform frontRightWheelTransform;
    [SerializeField]
    protected Transform rearLeftWheelTransform;
    [SerializeField]
    protected Transform rearRightWheelTransform;

    protected WheelCollider[] allWheels = new WheelCollider[4];
    protected Transform[] allTransforms = new Transform[4];

    protected bool isBoosted;
    [SerializeField]
    protected float boostDuration;
    protected float boostTimer;

    public Transform checkPoint;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if(driveType == DriveType.frontWheelDrive)
        {
            allWheels[0] = frontLeftWheelCol;
            allWheels[1] = frontRightWheelCol;
            allWheels[2] = rearLeftWheelCol;
            allWheels[3] = rearRightWheelCol;

            allTransforms[0] = frontLeftWheelTransform;
            allTransforms[1] = frontRightWheelTransform;
            allTransforms[2] = rearLeftWheelTransform;
            allTransforms[3] = rearRightWheelTransform;
        }
        else
        {
            allWheels[0] = rearLeftWheelCol;
            allWheels[1] = rearRightWheelCol;
            allWheels[2] = frontLeftWheelCol;
            allWheels[3] = frontRightWheelCol;

            allTransforms[0] = rearLeftWheelTransform;
            allTransforms[1] = rearRightWheelTransform;
            allTransforms[2] = frontLeftWheelTransform;
            allTransforms[3] = frontRightWheelTransform;
        }
        rigidBody.centerOfMass = CenterOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void SteerCar();
    protected abstract void AccelerateCar();
    public abstract void OnBoostPanel();

    public virtual void Respawn()
    {
        transform.position = checkPoint.position;
        transform.rotation = checkPoint.rotation;
        rigidBody.velocity = Vector3.zero;
    }

    protected void UpdateSingleWheel(WheelCollider wheel, Transform transform)
    {
        Vector3 newPos;
        Quaternion newRot;
        wheel.GetWorldPose(out newPos, out newRot);
        transform.position = newPos;
        transform.rotation = newRot;
    }


}
