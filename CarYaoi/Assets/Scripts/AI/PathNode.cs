using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode next;
    public PathNode prev;
    public PathNode GetNext(int numToSkip)
    {
        if(numToSkip == 0)
        {
            return next;
        }
        else
        {
            return next.GetNext(numToSkip - 1);
        }
    }

    public PathNode GetPrev(int numToSkip)
    {
        if (numToSkip == 0)
        {
            return prev;
        }
        else
        {
            return prev.GetPrev(numToSkip - 1);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(next != null)
        {
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
        
    }
}
