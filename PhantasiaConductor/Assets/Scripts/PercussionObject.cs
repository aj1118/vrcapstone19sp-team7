using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip loopClip;
    public Animation hitAnim;
    public Animation loopAnim;
    public float timePerBeat;
    public bool[] beats;
    // Start is called before the first frame update

    private BeatBlinkController beatBlinkController;
    private AudioSource hitSource;
    private AudioSource loopSource;
    private AudioSourceLoop audioSourceLoop;
    private Hittable hittable;
    private BeatInfo beatInfo;

    // We can remove this and set values in the prefab
    void Awake()
    {
        beatBlinkController = GetComponent<BeatBlinkController>();
  			
        hitSource = transform.parent.transform.Find("HitSource").GetComponent<AudioSource>();
        loopSource = transform.parent.transform.Find("LoopSource").GetComponent<AudioSource>();
        //adds 3d(ish) sound
        loopSource.spatialBlend = 1.0f;
        hitSource.spatialBlend = 1.0f;
        audioSourceLoop = transform.parent.transform.Find("AudioSourceLoop").GetComponent<AudioSourceLoop>();



        hitSource.clip = hitClip;
        loopSource.clip = hitClip;


        hittable = GetComponent<Hittable>();
        uint numBeats = 0;
        for (uint i = 0; i < beats.Length; i++) {
        		if (beats[i]){
        			numBeats++;
        		}
        }
        hittable.hitsBeforeBroadcast = numBeats;
        beatInfo = transform.parent.transform.Find("BeatInfo").GetComponent<BeatInfo>();
        beatInfo.beats = beats;
        beatInfo.timePerBeat = timePerBeat;
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
