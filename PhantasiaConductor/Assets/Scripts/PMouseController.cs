using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMouseController : MonoBehaviour
{
    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2)))
        {
            GameObject go = hit.collider.gameObject;

            if (Input.GetMouseButtonDown(0)) {
                go.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            }

            go.SendMessage("OnTracked", SendMessageOptions.DontRequireReceiver);
        }
    }
}
