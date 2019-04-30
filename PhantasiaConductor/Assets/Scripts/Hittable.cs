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
            canHit = GetComponent<PObject>().IsAlive();
        }
    }

    void OnHit()
    {
        Debug.Log(canHit);
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

    public void StopHit()
    {
        Debug.Log("INVOKED");
        canHit = false;

    }

    public bool CanHit {
        
        get {
            return canHit;
        }

        set {
            Debug.Log("SET TO " + value);
            canHit = value;
        }
    }
}
