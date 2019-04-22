using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    public uint hitsBeforeBroadcast;
    private uint hitCount;

    public bool broadcastUp = false;
    private bool canHit = true;

    void Start()
    {
        if (GetComponent<PObject>() != null) {
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
                BroadcastMessage("ObjectHit", SendMessageOptions.DontRequireReceiver);
            }
            Debug.Log("i was hit " + hitCount + " times");
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
