using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public SubscriberList notifyList;
    public SubscriberList hitOnceNotifyList;

    public uint hitsBeforeBroadcast;

    public UnityEvent onHitOnce;
    public UnityEvent onHitMultiple;
    
    private uint hitCount;

    public bool broadcastUp = false;
    private bool canHit = true;

    void Start()
    {
        if (GetComponent<PObject>() != null)
        {
            canHit = GetComponent<PObject>().IsAlive();
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
        canHit = true;
    }

    void OnDead()
    {
        canHit = false;
    }
}
