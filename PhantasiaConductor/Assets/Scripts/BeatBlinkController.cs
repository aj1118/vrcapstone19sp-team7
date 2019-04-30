using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlinkController : MonoBehaviour
{

    private Blink blink;
    public BeatInfo beatInfo;

    public bool wrapAround = true;

    private int beatCount = 0;

    void Awake()
    {
        blink = GetComponent<Blink>();
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
        bool bitValue = beatInfo.bitArray[beatCount];

        if (bitValue)
        {
            blink.BlinkOnOnce();
        }
        else
        {
            blink.BlinkOffOnce();
        }

        beatCount++;

        if (wrapAround) {
            beatCount = beatCount % beatInfo.bitArray.Length;
        }

        if (beatCount < beatInfo.bitArray.Length)
        {
            Invoke("RunBeat", beatInfo.timePerBeat);
        }

    }

}
