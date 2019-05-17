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

    public bool isPiano = false;

    // true is on, false is off
    private bool blinkState = true;
    private bool unlocked = false;

    private void Start()
    {
        if (isPiano) { 
            GetComponent<Collider>().enabled = true;
        }
        GetComponent<Renderer>().enabled = false;
    }


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
            if (isPiano)
            {
                GetComponent<Collider>().enabled = true;
            }
        
            onBlinkOn.Invoke();
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
        if (isPiano)
        {
            GetComponent<Collider>().enabled = false;
        }

        onBlinkOff.Invoke();
    }



}
