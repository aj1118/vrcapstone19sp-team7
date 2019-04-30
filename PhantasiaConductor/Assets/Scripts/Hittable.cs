using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public uint hitsBeforeBroadcast;

    public UnityEvent onHitOnce;
    public UnityEvent onHitMultiple;

    public bool canHit;

    public bool broadcastUp = false;

    private uint hitCount;


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

            hitCount++;
            if (hitCount % hitsBeforeBroadcast == 0)
            {
                // BroadcastMessage("ObjectHit", SendMessageOptions.DontRequireReceiver);

                // if (notifyList != null)
                // {
                //     notifyList.NotifyAll("ObjectHit");
                // }
                onHitMultiple.Invoke();
            }

            // if (hitOnceNotifyList != null)
            // {
            //     hitOnceNotifyList.NotifyAll("ObjectHitOnce");
            // }

            onHitOnce.Invoke();
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
            Debug.Log("set?");
            canHit = value;
        }
    }
}
