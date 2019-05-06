using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public uint hitsToUnlock;

    public UnityEvent onHitOnce;
    public UnityEvent onUnlock;

    public bool canHit;

    public bool broadcastUp = false;

    // keep track of hit counts
    private uint hitCount;

    // use hit flag to keep track of hits
    private bool hitFlag;



    void Start()
    {
        if (GetComponent<PObject>() != null)
        {
            // canHit = GetComponent<PObject>().IsAlive();
        }
    }

    void OnHit()
    {
        if (canHit)
        {
            canHit = false;
            hitCount++;
            onHitOnce.Invoke();
            if (hitCount % hitsToUnlock == 0)
            {
                onUnlock.Invoke();
            }

            
            HitFlag = true;
        }
    }

    void OnAlive()
    {
        // CanHit = true;
    }

    void OnDead()
    {
        // CanHit = false;
    }

    public void StopHit()
    {
        canHit = false;
    }

    public bool CanHit
    {
        get
        {
            return canHit;
        }
        set
        {
            canHit = value;
        }
    }

    // Resets the hit count if the hitflag is not set
    public void ResetIfHitFlagNotSet() {
        if (!HitFlag) {
            HitCount = 0;
        }
    }

    public bool HitFlag
    {
        get
        {
            return hitFlag;
        }

        set
        {
            hitFlag = value;
        }
    }

    public uint HitCount {
        get {
            return hitCount;
        }

        set {
            hitCount = value;
        }
    }
}
