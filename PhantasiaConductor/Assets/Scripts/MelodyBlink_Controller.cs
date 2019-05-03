using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyBlink_Controller : MonoBehaviour
{
    private BlinkDirection blink;
    public BeatInfo beatInfo;

    public bool wrapAround = true;

    private int beatCount = 0;
    private bool playingNote = false;

    void Awake()
    {
        blink = GetComponent<BlinkDirection>();
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
            if (!playingNote)
            {
                blink.BlinkOnOnce();
                if (beatInfo.noteArray[beatCount] > 1)
                {
                    playingNote = true;
                }
            }
        }
        else
        {
            blink.BlinkOffOnce();
            playingNote = false;
        }

        beatCount++;

        if (wrapAround)
        {
            beatCount = beatCount % beatInfo.bitArray.Length;
        }

        if (beatCount < beatInfo.bitArray.Length)
        {
            Invoke("RunBeat", beatInfo.timePerBeat);
        }

    }
}
