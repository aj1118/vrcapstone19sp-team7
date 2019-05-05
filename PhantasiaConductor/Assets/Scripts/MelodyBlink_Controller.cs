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
        bool bitValue = beatInfo.beats[beatCount];

        if (bitValue)
        {
            if (!playingNote)
            {
                blink.BlinkOnOnce();
                if (beatInfo.notes[beatCount] > 1)
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
            beatCount = beatCount % beatInfo.beats.Length;
        }

        if (beatCount < beatInfo.beats.Length)
        {
            Invoke("RunBeat", beatInfo.timePerBeat);
        }

    }
}
