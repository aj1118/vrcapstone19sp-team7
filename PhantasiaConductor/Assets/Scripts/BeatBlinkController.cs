using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlinkController : MonoBehaviour
{

    private Blink blink;
    public BeatInfo beatInfo;

    private int beatCount = 0;

    void Awake()
    {
        blink = GetComponent<Blink>();
        Debug.Log(blink);
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
        Debug.Log(bitValue);

        if (bitValue)
        {
            blink.BlinkOnOnce();
        }
        else
        {
            blink.BlinkOffOnce();
        }

        beatCount++;

        if (beatCount < beatInfo.bitArray.Length)
        {
            Invoke("RunBeat", beatInfo.timePerBeat);
        }

    }

}
