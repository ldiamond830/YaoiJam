using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{

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

        Debug.Log(angle);

        if (signedAngle < 0) {
            renderer.flipX = true;
        } else {
            renderer.flipX = false;
        }

        // i think i'd rather edit the animator controller floats directly
        // but i'm lazy and following the tutorial, so this will do for now
        // W
        if (angle < 25f || angle > 335f) {
            animationAngle = new Vector2(0f, -1f);
        } else if (angle < 65f) /*NW*/ {
            animationAngle = new Vector2(-0.5f, -0.5f);
        } else if (angle < 115f) /*N*/ {
            animationAngle = new Vector2(-1f, 0f);
        } else if (angle < 155f) /*NE*/ {
            animationAngle = new Vector2(-0.5f, 0.5f);
        } else if (angle < 205f) /*E*/ {
            animationAngle = new Vector2(0f, 1f);
        } else if (angle < 245f) /*SE*/ {
            animationAngle = new Vector2(0.5f, 0.5f);
        } else if (angle < 295f) /*S*/ {
            animationAngle = new Vector2(1f, 0f);
        } else if (angle < 335f) /*SW*/ {
            animationAngle = new Vector2(0.5f, -0.5f);
        } 

        

        animator.SetFloat("moveX", animationAngle.x);
        animator.SetFloat("moveY", animationAngle.y);
    }
}
