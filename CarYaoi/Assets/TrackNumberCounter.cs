using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackNumberCounter : MonoBehaviour
{
    public static TrackNumberCounter Instance;
    static int beenHereBefore;

    // Start is called before the first frame update
    void Start()
    {
        // copied from mechanic script
        // set up singleton to track how many times you've been to the track
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; DontDestroyOnLoad(gameObject); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool finishedAllTracks() {
        if (beenHereBefore >= 3) return true;
        return false;
    }

    public int howManyTimes() {
        return beenHereBefore;
    }

    public void beenHere() {
        beenHereBefore++;
    }
}
