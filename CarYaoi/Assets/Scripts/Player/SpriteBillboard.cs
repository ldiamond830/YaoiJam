// Moss Limpert
// https://youtu.be/FjJJ_I9zqJo?si=q9yrVuUSjA_lon_L
// https://www.youtube.com/watch?v=UcYfEfJW_mk
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    
    [SerializeField] bool freezeXZAxis = true;

    // Update is called once per frame
    void LateUpdate()
    {

        if (freezeXZAxis) {
            transform.rotation = Quaternion.Euler(0f,Camera.main.transform.rotation.eulerAngles.y,0f);
        } else {
            transform.rotation = Quaternion.Inverse(Camera.main.transform.rotation);
        }
        
    }
}
