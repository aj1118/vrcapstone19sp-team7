using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blink : MonoBehaviour
{
    public UnityEvent onBlinkOn;
    public UnityEvent onBlinkOff;
    
    // when blink change change state on/off
    public UnityEvent onBlinkOnToOff;
    public UnityEvent onBlinkOffToOn;

    // true is on, false is off
    private bool blinkState = true;
    private bool unlocked = false;


    public void BlinkOnOnce()
    {
        if (!unlocked)
        {
            if (!blinkState)
            {
                onBlinkOffToOn.Invoke();
            }
            blinkState = true;


            GetComponent<Renderer>().enabled = true;
            onBlinkOn.Invoke();
            // pObj.SendMessage(onEventName, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void BlinkOffOnce()
    {
        
        if (blinkState)
        {
            onBlinkOnToOff.Invoke();
        }
        blinkState = false;

        GetComponent<Renderer>().enabled = false;

        onBlinkOff.Invoke();
        // pObj.SendMessage(offEventName, SendMessageOptions.DontRequireReceiver);
    }



}
