using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    public float acceleration;
    public float braking;
    public float topSpeed;
    public float turnSpeed;
    public int boosts;
}

public enum PartType
{
    wheel,
    engine,
    body,
    spoiler
}

[CreateAssetMenu(fileName = "New Part", menuName = "Part", order = 1)]
public class PartScriptableObject : ScriptableObject
{
    public new string name;
    public int tier;
    public PartType type;
    public Stats stats;
    public Sprite image;

    public void Print ()
    {
        string temp;
        switch (type)
        {
            case PartType.wheel: temp = "wheel"; break;
            case PartType.engine: temp = "engine"; break;
            case PartType.body: temp = "body"; break;
            case PartType.spoiler: temp = "spoiler"; break;
            default: temp = "NULL"; break;
        }
        Debug.Log(name + ": a tier " + tier + " " + temp);
    }
}
