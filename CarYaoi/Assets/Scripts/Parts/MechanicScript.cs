using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton that stores all the parts in the game.
/// Holds functionality to get parts of a certain tier
/// </summary>
public class MechanicScript : MonoBehaviour
{
    public static MechanicScript instance;

    //Public mainly just for debugging purposes
    public PartScriptableObject[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        // Setup singleton
        if(instance != null && instance != this) { Destroy(this); }
        else { instance = this; }

        // Get all parts
        inventory = GetAllInstances<PartScriptableObject>();
    }

    // Update is called once per frame
    void Update() { }

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
    public List<PartScriptableObject> GetPartsOfTier(int tier)
    {
        List<PartScriptableObject> temp = new List<PartScriptableObject>();
        foreach (PartScriptableObject part in inventory)
        {
            if(part.tier == tier) { temp.Add(part); }
        }
        return temp;
    }

}
