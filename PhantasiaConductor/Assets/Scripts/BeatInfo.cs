using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatInfo : MonoBehaviour
{
    public int numBeats = 8;

    public float hittableWindowBefore = 0f;
    public float hittableWindowAfter = 0f;

    public float timePerBeat = 1f;

    public bool[] bitArray = {true, false, true, false, false, false, true, false};

}
