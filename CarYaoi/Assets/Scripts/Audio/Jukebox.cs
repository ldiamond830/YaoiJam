using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Jukebox : MonoBehaviour
{
    // Fields
    [SerializeField] private AudioClip intro;
    [SerializeField] private float introTailLength;
    [SerializeField] private AudioClip mainLoop;
    [SerializeField] private float mainLoopTailLength;
    private AudioSource[] sources;
    private int srcIndex = 1;
    private double dspTime;
    private double nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponentsInChildren<AudioSource>();

        // Start intro and calculate when it will end

        sources[0].clip = intro != null ? intro : mainLoop;
        dspTime = AudioSettings.dspTime;
        sources[0].PlayScheduled(dspTime + 1f);
        if (intro != null ){
                nextEventTime = dspTime + 1f + intro.length - introTailLength;
        }
        else {
                nextEventTime = dspTime + 1f + mainLoop.length - mainLoopTailLength;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        dspTime = AudioSettings.dspTime;

        // ~1 sec before playing clip ends, schedule next clip on open AudioSource
        if (dspTime + 1f > nextEventTime)
        {
            sources[srcIndex].clip = mainLoop;
            sources[srcIndex].PlayScheduled(nextEventTime);
            nextEventTime = nextEventTime + mainLoop.length - mainLoopTailLength;
            srcIndex = 1 - srcIndex;
        }
    }
}
