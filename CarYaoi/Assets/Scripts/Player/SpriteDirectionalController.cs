using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    [SerializeField] float forwardAngle = 65f;
    [SerializeField] float sideAngle = 155f;
    [SerializeField] float forwardLeft = 20f;
    [SerializeField] float forwardRight = 80f;


    [SerializeField] 
    Transform carTransform;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer renderer;


    
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);

        float signedAngle = Vector3.SignedAngle(carTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationAngle = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);
    }
}
