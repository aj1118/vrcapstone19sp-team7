using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ChordController : MonoBehaviour
{
    public GameObject[] targets;
    public int[] numBeats;

    public UnityEvent onCompleteChord;

    public Material Glow;
    public Material Hit;
    public Material Default;
    private BeatInfo beatInfo;
    private int[] beatCount;
    private int beatIndex = -1;
    private int targetIndex = 0;
    private int hitCount = 0;
    public float wait = 5f;
    private bool waitForLoop = false;
    private bool notePlaying = false;
    private bool complete = false;
    private bool invokeComplete = false;

    private void Awake()
    {
        beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();
        beatCount = new int[targets.Length];
    }

    private void OnEnable()
    {
        waitForLoop = true;
        RunTarget();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void NewLoop()
    {
        if (!complete)
        {
            beatIndex = -1;
            targetIndex = 0;
            waitForLoop = false;
            for (int i = 0; i < beatCount.Length; i++)
            {
                beatCount[i] = 0;
            }
        } else if (invokeComplete)
        {
            invokeComplete = false;
            onCompleteChord.Invoke();
        }
        
    }

    private void RunTarget()
    {
        if (!waitForLoop)
        {
            bool nextBeat = beatInfo.beats[(beatIndex + 1) % beatInfo.beats.Length];
            beatIndex++;

            if (nextBeat)
            {
                // Start glowing target
                if (!notePlaying)
                {
                    //targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = true;
                    targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().material = Glow;
                    notePlaying = true;
                    Invoke("HitTime", wait);
                }
            }
            else if (notePlaying)
            {
                // Stop glowing target 
                // Invoke("GlowOff", beatInfo.beatTime * beatInfo.hittableAfter);
                //targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = false;
                targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().material = Default;
                // increment target index
                beatCount[targetIndex]++;
                notePlaying = false;
  
                if (beatCount[targetIndex] == numBeats[targetIndex])
                {
                    // targetIndex++;
                    targetIndex = (targetIndex + 1) % targets.Length;
                }   
            }

            // May need to move this to beginning of method
            if (targetIndex == targets.Length)
            {
                complete = true;
                invokeComplete = true;
            }
        }
        if (targetIndex < targets.Length)
        {
            Invoke("RunTarget", beatInfo.beatTime);
        }
    }

    /*void GlowOn()
    {
        targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = true;
    }

    void GlowOff()
    {
        targets[targetIndex].transform.Find("TargetObject").GetComponent<Renderer>().enabled = false;
    }*/
    public void ResetTargets()
    {
        NewLoop();
        waitForLoop = true;
    }
    private void HitTime()
    {
        if(targets[targetIndex].GetComponent<Collision>().contactCount != hitCount)
        {

        }

    }
    
}
