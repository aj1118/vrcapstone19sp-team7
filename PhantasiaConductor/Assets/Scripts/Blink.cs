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

    public UnityEvent onBlinkOn;
    public UnityEvent onBlinkOff;
    
    // when blink change change state on/off
    public UnityEvent onBlinkOnToOff;
    public UnityEvent onBlinkOffToOn;

    // invoked every blink tick
    public UnityEvent onBlinkTick;

    

    // true is on, false is off
    private bool blinkState = true;

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
        if (!blinkState) {
            onBlinkOffToOn.Invoke();
        }
        blinkState = true;

        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Alive();
        onBlinkOn.Invoke();
        // pObj.SendMessage(onEventName, SendMessageOptions.DontRequireReceiver);

    }

    public void BlinkOffOnce()
    {
        if (blinkState) {
            onBlinkOnToOff.Invoke();
        }
        blinkState = false;

        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Dead();
        onBlinkOff.Invoke();
        // pObj.SendMessage(offEventName, SendMessageOptions.DontRequireReceiver);
    }

    void OnEnable()
    {
        if (blinkOnTime >= 0)
        {
            Invoke("BlinkOn", blinkOnTime);
        }
    }


}
