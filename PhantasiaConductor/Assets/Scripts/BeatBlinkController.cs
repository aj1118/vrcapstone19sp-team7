using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlinkController : MonoBehaviour
{

    public BeatInfo beatInfo;
    private float hittableBefore;
    private float hittableAfter;
    private float timePerBeat;

    public bool wrapAround = true;

    private Blink blink;
    private int beatCount = 0;

    void Awake()
    {
        blink = GetComponent<Blink>();
        hittableBefore = beatInfo.hittableBefore;

        hittableAfter = beatInfo.hittableBefore;
        timePerBeat = beatInfo.timePerBeat;
    }


    void OnEnable()
    {
        RunBeat();
    }

    void OnDisable()
    {
        CancelInvoke();
    }


    void RunBeat()
    {
        bool nextBeat = beatInfo.beats[(beatCount + 1) % beatInfo.beats.Length];

        if (nextBeat)
        {
            Invoke("BlinkOn", timePerBeat - (hittableBefore * timePerBeat));
        }
        else
        {
            Invoke("BlinkOff", hittableAfter);
        }

        beatCount++;

        if (wrapAround) {
            beatCount = beatCount % beatInfo.beats.Length;
        }

        if (beatCount < beatInfo.beats.Length)
        {
            Invoke("RunBeat", beatInfo.timePerBeat);
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

}
