using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blink : MonoBehaviour
{

    // time before blinking on, if negative, then it will not automatically blink
    public float blinkOnTime = -1;
    // time before blinking off
    public float blinkOffTime = -1;

    public SubscriberList onSubscribers;
    public SubscriberList offSubscribers;

    public UnityEvent onBlinkOn;
    public UnityEvent onBlinkOff;
    
    // Start is called before the first frame update

    // true is on, false is off
    private bool blinkState;

    private const string onEventName = "OnBlinkOn";
    private const string offEventName = "OnBlinkOff";
    void BlinkOn()
    {
        Invoke("BlinkOff", blinkOffTime);
        BlinkOnOnce();
    }

    void BlinkOff()
    {
        Invoke("BlinkOn", blinkOnTime);
        BlinkOffOnce();
    }

    public void BlinkOnOnce()
    {
        blinkState = true;

        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Alive();
        pObj.SendMessage(onEventName, SendMessageOptions.DontRequireReceiver);

        if (onSubscribers != null)
        {
            onSubscribers.NotifyAll(onEventName);
        }
    }

    public void BlinkOffOnce()
    {
        blinkState = false;

        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Dead();
        pObj.SendMessage(offEventName, SendMessageOptions.DontRequireReceiver);

        if (offSubscribers != null)
        {
            offSubscribers.NotifyAll(offEventName);
        }

    }

    void OnEnable()
    {
        if (blinkOnTime >= 0)
        {
            Invoke("BlinkOn", blinkOnTime);
        }
    }


}
