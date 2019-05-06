using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordController : MonoBehaviour
{
    public int numTargets;
    public GameObject[] targets;

    private BeatInfo beatInfo;
    private int noteIndex = 0;
    private int beatIndex = 0;
    private bool notePlaying = false;

    private void Awake()
    {
        beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();
    }

    private void OnEnable()
    {
        RunTargets();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void RunTargets()
    {
        bool nextBeat = beatInfo.beats[(beatIndex) % beatInfo.beats.Length];
        int noteLength = beatInfo.notes[(beatIndex) % beatInfo.notes.Length];

        if (nextBeat)
        {
            if (!notePlaying)
            {
                // make target glow
            }
            if (noteLength > 1)
            {
                notePlaying = true;
            } else
            {
                notePlaying = false;
            }
        }
        else
        {
            // Stop glowing target 
        }

        beatIndex++;

        if (beatIndex < beatInfo.beats.Length)
        {
            Invoke("RunTarget", beatInfo.beatTime);
        }
    }
}
