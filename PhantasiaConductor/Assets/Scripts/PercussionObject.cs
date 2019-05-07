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
    private Hittable hittable;
    private BeatInfo beatInfo;
    private Loop loop;

    // We can remove this and set values in the prefab
    void Awake()
    {
        beatBlinkController = GetComponent<BeatBlinkController>();
  		  loop = GetComponent<Loop>();
        hitSource = transform.parent.transform.Find("HitSource").GetComponent<AudioSource>();
        loopSource = transform.parent.transform.Find("LoopSource").GetComponent<AudioSource>();
        loopSource.pitch = loopClip.length / MasterLoop.loopTime;
        Debug.Log(loopClip.length);
        //adds 3d(ish) sound
        loopSource.spatialBlend = 1.0f;
        hitSource.spatialBlend = 1.0f;
        
        hitSource.clip = hitClip;
        loopSource.clip = loopClip;


        hittable = GetComponent<Hittable>();
        
        beatInfo = transform.parent.transform.Find("BeatInfo").GetComponent<BeatInfo>();

        hittable.hitsToUnlock = hitsToUnlock;
        beatBlinkController.beatInfo = beatInfo;
    }

    
    public void Unlock()
    {
    	Invoke("LoopSourceOn", hitClip.length + .1f);
    }

    void LoopSourceOn(){
    	loopSource.volume = 1.0f;
    }

    public void NewLoop()
    {
        beatBlinkController.NewLoop();
        loopSource.Play();
 
    }
}
