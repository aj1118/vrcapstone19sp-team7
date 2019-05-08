using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatBlinkController : MonoBehaviour
{

    public UnityEvent onHitUnlocked; // A 'hit' that plays  once a track is unlocked
    private Blink blink;
    public BeatInfo beatInfo;
    public bool unlocked;
    private int beatCount = 0;

    void Awake()
    {
        unlocked = false;
        blink = GetComponent<Blink>();
    }

    public void Unlock()
    {
        unlocked = true;
    }

    public void NewLoop()
    {
        beatCount = -1;
        RunBeat();
    }
 
    void RunBeat()
    {
        
        beatCount++;
        if (beatCount < beatInfo.beats.Length - 1)
        {
            Invoke("RunBeat", beatInfo.beatTime);
        }
        if (unlocked)
        {
            if (beatInfo.beats[beatCount])
            {
                onHitUnlocked.Invoke();
            }
        }
        else
        {
            bool nextBeat = beatInfo.beats[(beatCount + 1) % beatInfo.beats.Length];
            if (nextBeat)
            {
                Invoke("BlinkOn", beatInfo.beatTime - (beatInfo.beatTime * beatInfo.hittableBefore));
            }
            else
            {
                Invoke("BlinkOff", beatInfo.beatTime * beatInfo.hittableAfter);
            }
        }
        
    }
    
    void BlinkOn()
    {
        blink.BlinkOnOnce();
    }

    void BlinkOff()
    {
        blink.BlinkOffOnce();
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
