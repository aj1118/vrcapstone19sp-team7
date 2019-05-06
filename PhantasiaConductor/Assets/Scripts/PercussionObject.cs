using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip loopClip;

    public uint hitsToUnlock;
    public Animation hitAnim;
    public Animation loopAnim;
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
        
        beatInfo = transform.parent.transform.Find("BeatInfo").GetComponent<BeatInfo>();

        hittable.hitsToUnlock = hitsToUnlock;
        
        audioSourceLoop.beatInfo = beatInfo;
        audioSourceLoop.source = loopSource;

        beatBlinkController.beatInfo = beatInfo;
    }

    

    void Start()
    {



    }

    public void NewLoop()
    {
        beatBlinkController.NewLoop();
        loopSource.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
