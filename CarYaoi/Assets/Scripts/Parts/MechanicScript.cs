using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicScript : MonoBehaviour
{
    public PartScriptableObject[] inventory;
    public PartScriptableObject[] availableStock;
    public int maxTier = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get all parts
        inventory = GetAllInstances<PartScriptableObject>();
        availableStock = GetAvailableParts();

        // Not sure how this will be done, or if it will even be a function of this class
        DisplayParts();
    }

    // Update is called once per frame
    void Update()
    {
        // If Click
        // PartSelected();
    }

    /// Gets all Parts in the game
    /// https://stackoverflow.com/questions/62102324/how-can-i-add-each-instance-of-a-scriptable-object-to-a-collection
    private static T[] GetAllInstances<T>() where T : PartScriptableObject
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }

    /// <summary>
    /// Gets all parts currently available to the player
    /// </summary>
    /// <returns></returns>
    private PartScriptableObject[] GetAvailableParts()
    {
        PartScriptableObject[] temp = { };
        foreach (PartScriptableObject part in inventory)
        {
            if(part.tier <= maxTier) { temp[temp.Length] = part; }
        }
        return temp;
    }

    //
    // Not Yet Implemented
    //
    private void DisplayParts() { }
    private void PartSelected() { }


}
