using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Singleton that stores all the parts in the game.
/// Holds functionality to get parts of a certain tier
/// </summary>
public class MechanicScript : MonoBehaviour
{
    public static MechanicScript Instance;

    //Public mainly just for debugging purposes
    public PartScriptableObject[] stock;
    public List<PartScriptableObject> playerInventory;
    public Stats playerStats;
    public int maxSlots = 10;

    LoadScene loadScene;

    [SerializeField]
    string sceneToLoad;

    // Awake is called before the Start function
    void Awake()
    {
        loadScene = new LoadScene();

        // Setup singleton
        if(Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; DontDestroyOnLoad(gameObject); }

        // Get all parts
        stock = (PartScriptableObject[])Resources.LoadAll<PartScriptableObject>("Parts");
        playerStats.pointCost = maxSlots;
    }

    // Update is called once per frame
    void Update() { 
        
    }

    /// <summary>
    /// Gets all parts currently available to the player
    /// </summary>
    /// <returns></returns>
    public List<PartScriptableObject> GetPartsOfTier(int tier)
    {
        List<PartScriptableObject> temp = new List<PartScriptableObject>();
        foreach (PartScriptableObject part in stock)
        {
            if(part.tier == tier) { temp.Add(part); }
        }
        return temp;
    }

    /// <summary>
    /// Add desired part to player's inventory if player has available slots
    /// </summary>
    /// <param name="part">Part to add to player's inventory</param>
    /// <returns>True if added, false if not able to be added</returns>
    public bool AddPartToPlayerInventory(PartScriptableObject part)
    {
        if ( playerInventory.Count >= 3) {
            loadScene.OnClickLoadScene(sceneToLoad);
        }

        // If part type already exists, replace that part type
        foreach (PartScriptableObject p in playerInventory)
        {
            if(p.type == part.type) 
            { 
                if(part.stats.pointCost <= (playerStats.pointCost + p.stats.pointCost))
                {
                    playerInventory.Remove(p);
                    playerInventory.Add(part);
                    ComputePlayerStats();
                    return true;
                }
                return false;
            }
        }
        // If part type not already in inventory, check if can be added
        if (part.stats.pointCost <= playerStats.pointCost)
        {
            playerInventory.Add(part);
            ComputePlayerStats();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Helper function to calculate the player's stats based on their inventory
    /// </summary>
    /// <returns>The player's stats</returns>
    public Stats ComputePlayerStats()
    {
        Stats stats = new Stats();
        stats.pointCost = maxSlots;
        foreach (PartScriptableObject p in playerInventory)
        {
            Stats partStats = p.stats;
            stats.acceleration += partStats.acceleration;
            stats.braking += partStats.braking;
            stats.topSpeed += partStats.topSpeed;
            stats.turnSpeed += partStats.turnSpeed;
            stats.boosts += partStats.boosts;
            stats.pointCost -= partStats.pointCost;
        }
        playerStats = stats;
        return playerStats;
    }

    // returns the number of points a player can allocate to buy parts
    public int Slots()
    {
        Stats playerStats = ComputePlayerStats();
        return playerStats.pointCost;
    }
}
