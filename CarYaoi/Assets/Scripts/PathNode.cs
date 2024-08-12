using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode next;


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(next != null)
        {
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
        
    }
}
