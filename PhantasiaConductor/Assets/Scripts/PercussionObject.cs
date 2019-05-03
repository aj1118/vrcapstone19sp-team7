using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{
    public AudioClip audioClip;
    // Start is called before the first frame update

    private BeatBlinkController beatBlinkController;

    private AudioSource audioSource;

    private AudioSource audioSourceLoop;

    private AudioSourceLoop audioLoop;

    private Hittable hittable;

    private BeatInfo beatInfo;

    void Awake()
    {
        beatBlinkController = GetComponent<BeatBlinkController>();
    
        audioSource = transform.parent.transform.Find("AudioSource").GetComponent<AudioSource>();
        audioSourceLoop = transform.parent.transform.Find("AudioSourceLoop").GetComponent<AudioSource>();
        audioLoop = transform.parent.transform.Find("AudioLoop").GetComponent<AudioSourceLoop>();

        audioSource.clip = audioClip;
        audioSourceLoop.clip = audioClip;

        hittable = GetComponent<Hittable>();
        beatInfo = transform.parent.transform.Find("BeatInfo").GetComponent<BeatInfo>();

        audioLoop.beatInfo = beatInfo;
        audioLoop.source = audioSourceLoop;

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
