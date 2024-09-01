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
    SpriteRenderer spriteRenderer;


    
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);

        float signedAngle = Vector3.SignedAngle(carTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationAngle = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);

        //Debug.Log("angle:" + angle);

        if (signedAngle < 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
        
        
        
        
        // i think i'd rather edit the animator controller floats directly
        // but i'm lazy and following the tutorial, so this will do for now
        // W SIDE
        if (angle < 3f || angle > 355f) {
            animationAngle = new Vector2(0f, 1f);
            

        } else if (angle < 90f) /*NW FWD LEFT*/ {
            animationAngle = new Vector2(0.75f, 0.75f);

        } else if (angle < 90.05f) /*N FWD*/ {
            animationAngle = new Vector2(1f, 0f);

        } else if (angle < 155f) /*NE FWD RIGHT*/ {
            animationAngle = new Vector2(0.75f, -0.75f);
        } else if (angle < 190f) /*E SIDE*/ {
            animationAngle = new Vector2(0f, -1f);

        } else if (angle < 225f) /*SE FWD LEFT*/ {
            animationAngle = new Vector2(-0.75f, -0.75f);

        } else if (angle < 295f) /*S FWD*/ {
            animationAngle = new Vector2(-1f, 0f);

        } else if (angle < 335f) /*SW FWD RIGHT*/ {
            
            animationAngle = new Vector2(-0.75f, 0.75f);

        } 

        

        animator.SetFloat("moveX", animationAngle.x);
        animator.SetFloat("moveY", animationAngle.y);
    }
}
