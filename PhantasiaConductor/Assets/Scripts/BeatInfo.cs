using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatInfo : MonoBehaviour
{
    public int numBeats = 8;

    // Percentage of timesPerBeat
    public float hittableBefore = 0.5f;
    public float hittableAfter = 0.5f;

    public float timePerBeat = 1f;

    public bool[] beats = {true, false, true, false, false, false, true, false};

    // For melody and other ntoes
    // noteArray[i] indicates for how many beats note i will be playing.
    // Example:
    //  bitArray = {true, true, ...}
    //  noteArray = {2, 0, ...}
    //  Means the first note will play for 2 beats 
    public int[] notes = { 1, 0, 1, 0, 0, 0, 1, 0 };
}
