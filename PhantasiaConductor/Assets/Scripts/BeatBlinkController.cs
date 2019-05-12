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
    private int hitCount = 0;
    private Vector3 originalPos;

    void Awake()
    {
        unlocked = false;
        blink = GetComponent<Blink>();
        originalPos = transform.position;
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
        bool isHit = beatInfo.beats[beatCount];
        bool isNextHit = beatInfo.beats[(beatCount + 1) % beatInfo.beats.Length];
        

        if (beatInfo.offsets.Length > 0)
        {
            transform.position += beatInfo.offsets[0];
        }

        if (unlocked)
        {
            if (isHit)
            {
                updateOffset();
                onHitUnlocked.Invoke();
            }
        }
        else
        {
            if (isNextHit)
            {
                Invoke("BlinkOn", beatInfo.beatTime - (beatInfo.beatTime * beatInfo.hittableBefore));
            }
            if (isHit)
            {
                Invoke("BlinkOff", beatInfo.beatTime * beatInfo.hittableAfter);
            }
        }

        if (beatCount < beatInfo.beats.Length - 1)
        {
            Invoke("RunBeat", beatInfo.beatTime);
        }
    }
    
    void BlinkOn()
    {
        blink.BlinkOnOnce();
    }

    void BlinkOff()
    {
        blink.BlinkOffOnce();
        updateOffset();
    }

    private void updateOffset()
    {
        if (beatInfo.offsets.Length != 0)
        {
            hitCount++;
            if (hitCount == beatInfo.offsets.Length)
            {
                hitCount = 0;
            }
            transform.position = originalPos + beatInfo.offsets[hitCount];
            
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}