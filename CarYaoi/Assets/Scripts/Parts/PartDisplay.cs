using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDisplay : MonoBehaviour
{
    public PartScriptableObject part;

    // Start is called before the first frame update
    void Start()
    {
        part.Print();
    }
}
