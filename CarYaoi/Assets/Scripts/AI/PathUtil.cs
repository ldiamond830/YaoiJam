using UnityEngine;
[ExecuteInEditMode]
public class PathUtil : MonoBehaviour
{
    private void Start()
    {
        PathNode[] nodes = GetComponentsInChildren<PathNode>();
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].next = nodes[(i + 1) % nodes.Length];
        }
    }

    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            PathNode[] nodes = GetComponentsInChildren<PathNode>();
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].next = nodes[(i + 1) % nodes.Length];

                if(i == 0)
                {
                    nodes[i].prev = nodes[nodes.Length - 1];
                }
                else
                {
                    nodes[i].prev = nodes[i - 1];
                }
            }
        }
    }
}
