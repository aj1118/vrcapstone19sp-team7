using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrOverlay : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        var frustrumHeight = 2.0f * .8f * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustrumWidth = frustrumHeight * camera.aspect;
        RectTransform t = GetComponent<RectTransform>();
        
        // add extra padding just in case
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, frustrumWidth + 0.1f);
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, frustrumHeight + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (camera.transform.forward * .8f) + camera.transform.position;
        transform.rotation = camera.transform.rotation;
    }
}
