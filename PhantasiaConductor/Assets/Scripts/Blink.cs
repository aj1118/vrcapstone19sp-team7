using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    // public float blinkTime;
    
    // time before blinking on
    public float blinkOnTime = 1;
    // time before blinking off
    public float blinkOffTime = 1;
    // Start is called before the first frame update

    // true is on, false is off
    private bool blinkState;


    void BlinkOn() {
        blinkState = true;
        
        Invoke("BlinkOff", blinkOffTime);
        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Alive();
        pObj.SendMessage("OnBlinkOn", SendMessageOptions.DontRequireReceiver);
    }

    void BlinkOff() {
        blinkState = false;
        
        Invoke("BlinkOn", blinkOnTime);
        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Dead();
        pObj.SendMessage("OnBlinkOff", SendMessageOptions.DontRequireReceiver);
    }

    void OnEnable()
    {
        Invoke("BlinkOn", blinkOnTime);
        // InvokeRepeating("DoBlink", blinkTime, blinkTime);
    }
}
