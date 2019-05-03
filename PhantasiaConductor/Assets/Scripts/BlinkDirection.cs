using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlinkDirection : MonoBehaviour
{
    // For drawing direction line
    public float directionLineLength;

    public UnityEvent onBlinkDirectionOn;
    public UnityEvent onBlinkDirectionOff;

    // when blink change change state on/off
    public UnityEvent onBlinkDirectionOnToOff;
    public UnityEvent onBlinkDirectionOffToOn;

    // true is on, false is off
    private bool blinkState = true;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // ** Uncomment to make the direction line taper **
        // lineRenderer.startWidth = 1f;
        // lineRenderer.endWidth = 0.5f;

        lineRenderer.enabled = false;
    }

    public void BlinkOnOnce()
    {
        if (!blinkState)
        {
            onBlinkDirectionOffToOn.Invoke();
        }
        blinkState = true;

        Vector3 endPosition = transform.position;
        endPosition.x = endPosition.x - directionLineLength;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);

        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Alive();
        lineRenderer.enabled = true;
      
        onBlinkDirectionOn.Invoke();
    }

    public void BlinkOffOnce()
    {
        if (blinkState)
        {
            onBlinkDirectionOnToOff.Invoke();
        }
        blinkState = false;

        lineRenderer.enabled = false;
        PObject pObj = gameObject.GetComponent<PObject>();
        pObj.Dead();
        onBlinkDirectionOff.Invoke();
    }
}
