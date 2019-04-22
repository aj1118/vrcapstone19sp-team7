using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    public float blinkTime;
    // Start is called before the first frame update

    // true is on, false is off
    private bool blinkState;

    void DoBlink()
    {
        blinkState = !blinkState;
        PObject pObj = gameObject.GetComponent<PObject>();
        
        if (blinkState) {
            pObj.Alive();
            SendMessage("OnBlinkOn", SendMessageOptions.DontRequireReceiver);
        } else {
            pObj.Dead();
            SendMessage("OnBlinkOff", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnEnable()
    {
        InvokeRepeating("DoBlink", blinkTime, blinkTime);
    }
}
