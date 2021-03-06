﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public uint hitsToUnlock;

    public UnityEvent onHitOnce;
    public UnityEvent onUnlock;
    public UnityEvent onPinched;
    public UnityEvent onTracked;

    public bool inContact;
    
    public bool canHit;
    public bool canInteract;
    public bool preventRepeated = true;

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

    void OnTriggerEnter()
    {
        if (canHit)
        {
            if (preventRepeated)
            {
                canHit = false;
            }

            onHitOnce.Invoke();
            hitCount++;
            //Debug.Log(hitCount + "       " + hitsToUnlock);
            if (hitCount == hitsToUnlock)
            {
                onUnlock.Invoke();
            }


            HitFlag = true;
        }
    }

    void OnPinched()
    {
        if (canInteract)
        {
            onPinched.Invoke();
        }
    }

    void OnTracked()
    {
        if (canInteract)
        {
            onTracked.Invoke();
        }
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
    public void ResetIfHitFlagNotSet()
    {
        if (hitCount != 0)
        {
            //Play miss sound quietly?
        }
        if (!HitFlag)
        {
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

    public uint HitCount
    {
        get
        {
            return hitCount;
        }

        set
        {
            hitCount = value;
        }
    }
}
