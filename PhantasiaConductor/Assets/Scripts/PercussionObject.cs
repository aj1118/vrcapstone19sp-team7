using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip audioClip;
    // Start is called before the first frame update

    private BeatBlinkController beatBlinkController;

    private AudioSource hitSource;

    private AudioSource loopSource;

    private AudioSourceLoop audioSourceLoop;

    private Hittable hittable;

    private BeatInfo beatInfo;

    void Awake()
    {
        beatBlinkController = GetComponent<BeatBlinkController>();
    
        hitSource = transform.parent.transform.Find("HitSource").GetComponent<AudioSource>();
        audioSourceLoop = transform.parent.transform.Find("AudioSourceLoop").GetComponent<AudioSourceLoop>();
        loopSource = transform.parent.transform.Find("LoopSource").GetComponent<AudioSource>();

        hitSource.clip = audioClip;
        loopSource.clip = audioClip;

        hittable = GetComponent<Hittable>();
        beatInfo = transform.parent.transform.Find("BeatInfo").GetComponent<BeatInfo>();

        audioSourceLoop.beatInfo = beatInfo;
        audioSourceLoop.source = loopSource;

        beatBlinkController.beatInfo = beatInfo;
    }
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }
}
