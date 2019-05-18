using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionObject : MonoBehaviour
{

    public AudioClip hitClip;
    public AudioClip loopClip;
    public Material unlockMaterial;
    public uint hitsToUnlock = 4;
    public bool unlocked = false;
    public bool isPiano = false;

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
        hitRenderer = transform.Find("HitAnimation").GetComponent<Renderer>();
        

        if (isPiano)
        {
            hitSource = GetComponent<AudioSource>();
            beatInfo = GetComponent<BeatInfo>();
        } else
        {

            hitSource = transform.Find("HitSource").GetComponent<AudioSource>();
            hitSource.spatialBlend = 1.0f;
            hitSource.clip = hitClip;
            loopSource = transform.Find("LoopSource").GetComponent<AudioSource>();
            loopSource.pitch = loopClip.length / MasterLoop.loopTime;
            loopSource.spatialBlend = 1.0f;
            loopSource.clip = loopClip;
            beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>(); 
        }

        hittable = GetComponent<Hittable>();
        
        hittable.hitsToUnlock = hitsToUnlock;
        beatBlinkController.beatInfo = beatInfo;
    }

    public void NewLoop()
    {
        if (gameObject.activeInHierarchy)
        {
            beatBlinkController.NewLoop();
            if (!isPiano)
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
        if (!unlocked || !isPiano)
        {
            hitSource.Play();
        }
    }
    void LoopSourceOn()
    {
    	loopSource.volume = 1.0f;
    }

    
}
