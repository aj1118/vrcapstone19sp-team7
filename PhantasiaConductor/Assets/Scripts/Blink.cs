using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    
    // time before blinking on
    public float blinkOnTime = 1;
    // time before blinking off
    public float blinkOffTime = 1;

    public SubscriberList onSubscribers;
    public SubscriberList offSubscribers;
    // Start is called before the first frame update

    // true is on, false is off
    private bool blinkState;

    private const string onEventName = "OnBlinkOn";
    private const string offEventName = "OnBlinkOff";
    void BlinkOn() {
        blinkState = true;
        
        Invoke("BlinkOff", blinkOffTime);
        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Alive();
        pObj.SendMessage(onEventName, SendMessageOptions.DontRequireReceiver);

        if (onSubscribers != null) {
            onSubscribers.NotifyAll(onEventName);
        }
    }

    void BlinkOff() {
        blinkState = false;
        
        Invoke("BlinkOn", blinkOnTime);
        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Dead();
        pObj.SendMessage(offEventName, SendMessageOptions.DontRequireReceiver);

        if (offSubscribers != null) {
            offSubscribers.NotifyAll(offEventName);
        }
    }

    void OnEnable()
    {
        Invoke("BlinkOn", blinkOnTime);
    }
}
