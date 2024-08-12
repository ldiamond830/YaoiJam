using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarController : MonoBehaviour
{
    [SerializeField]
    protected float motorPower;
    [SerializeField]
    protected float breakPower;
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


    // Start is called before the first frame update
    void Start()
    {
        allWheels[0] = frontLeftWheelCol;
        allWheels[1] = frontRightWheelCol;
        allWheels[2] = frontLeftWheelCol;
        allWheels[3] = frontRightWheelCol;

        allTransforms[0] = frontLeftWheelTransform;
        allTransforms[1] = frontRightWheelTransform;
        allTransforms[2] = frontLeftWheelTransform;
        allTransforms[3] = frontRightWheelTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void SteerCar();
    protected abstract void AccelerateCar();

    protected void UpdateSingleWheel(WheelCollider wheel, Transform transform)
    {
        Vector3 newPos;
        Quaternion newRot;
        wheel.GetWorldPose(out newPos, out newRot);
        transform.position = newPos;
        transform.rotation = newRot;
    }

}
