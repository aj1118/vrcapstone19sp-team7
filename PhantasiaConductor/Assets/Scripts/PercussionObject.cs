using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip loopClip;

    public uint hitsToUnlock;
    public Material unlockMaterial;
    // Start is called before the first frame update

    
    private Renderer hitRenderer;
    private BeatBlinkController beatBlinkController;
    private AudioSource hitSource;
    private AudioSource loopSource;
    private Hittable hittable;
    private BeatInfo beatInfo;

    // We can remove this and set values in the prefab
    void Awake()
    {
        beatBlinkController = GetComponent<BeatBlinkController>();
  	
        hitSource = transform.Find("HitSource").GetComponent<AudioSource>();
        loopSource = transform.Find("LoopSource").GetComponent<AudioSource>();
        hitRenderer = transform.Find("HitAnimation").GetComponent<Renderer>();
        loopSource.pitch = loopClip.length / MasterLoop.loopTime;
        
        //adds 3d(ish) sound
        loopSource.spatialBlend = 1.0f;
        hitSource.spatialBlend = 1.0f;
        
        hitSource.clip = hitClip;
        loopSource.clip = loopClip;


        hittable = GetComponent<Hittable>();
        
        beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();

        hittable.hitsToUnlock = hitsToUnlock;
        beatBlinkController.beatInfo = beatInfo;
    }

    public void NewLoop()
    {
        if (gameObject.activeInHierarchy)
        {
            beatBlinkController.NewLoop();
            loopSource.Play();
        }
    }

    public void Unlock()
    {
    	Invoke("LoopSourceOn", hitClip.length + .1f);
        hitRenderer.material = unlockMaterial;
        Debug.Log(GetComponent<MeshRenderer>().material);
    }

    void LoopSourceOn()
    {
    	loopSource.volume = 1.0f;
    }

    
}
