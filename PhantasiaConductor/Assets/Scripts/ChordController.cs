using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChordController : MonoBehaviour
{
    public GameObject[] targets;

    public UnityEvent onCompleteChord;

    private BeatInfo beatInfo;
    private int beatIndex = -1;
    private int targetIndex = 0;
    private bool waitForLoop = false;
    private bool notePlaying = false;

    private void Awake()
    {
        beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();
    }

    private void OnEnable()
    {
        RunTarget();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void NewLoop()
    {
        beatIndex = -1;
        targetIndex = 0;
        waitForLoop = false;
    }

    private void RunTarget()
    {
        if (waitForLoop)
        {
            if ((beatIndex + 1) % beatInfo.beats.Length != 0)
            {
                return;
            } else
            {
                waitForLoop = false;
            }
        }

        Debug.Log(beatIndex);

        bool nextBeat = beatInfo.beats[(beatIndex + 1) % beatInfo.beats.Length];
        int noteLength = beatInfo.notes[(beatIndex + 1) % beatInfo.notes.Length];

        if (nextBeat)
        {
            if (!notePlaying)
            {
                // Start glowing target
                targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = true;
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
            targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = false;

            // Change to targetIndex++
            targetIndex = (targetIndex + 1) % targets.Length;
        }

        // May need to move this to begining of method
        if (targetIndex == targets.Length)
        {
            onCompleteChord.Invoke();
        }

        beatIndex++;

        if (beatIndex < beatInfo.beats.Length)
        {
            Invoke("RunTarget", beatInfo.beatTime);
        }
    }

    public void ResetTargets()
    {
        targetIndex = 0;
        waitForLoop = true;
    }
}
