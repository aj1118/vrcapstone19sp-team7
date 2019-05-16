using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip loopClip;
    public Material unlockMaterial;
    public bool useLoop = true; //if false, will play hitclip on loop instead
    public uint hitsToUnlock = 4;
    public bool unlocked = false;

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
        hitRenderer = transform.Find("HitAnimation").GetComponent<Renderer>();


        //adds 3d(ish) sound

        hitSource.spatialBlend = 1.0f;
        hitSource.clip = hitClip;

        if (useLoop)
        {
            loopSource = transform.Find("LoopSource").GetComponent<AudioSource>();
            loopSource.pitch = loopClip.length / MasterLoop.loopTime;
            loopSource.spatialBlend = 1.0f;
            loopSource.clip = loopClip;
        }

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
            if (useLoop)
            {
                loopSource.Play();
            }
        }
    }

    public void Unlock()
    {
        unlocked = true;
        Invoke("LoopSourceOn", hitClip.length + .1f);
        hitRenderer.material = unlockMaterial;
        Debug.Log(GetComponent<MeshRenderer>().material);
    }

    public void HitOnce()
    {
        if (!unlocked || !useLoop)
        {
            hitSource.Play();
        }
    }
    void LoopSourceOn()
    {
    	loopSource.volume = 1.0f;
    }

    
}
