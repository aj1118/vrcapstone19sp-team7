using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class HandController : MonoBehaviour
    {
        public bool debugMode = true;

        public Hand leftHand;
        public Hand rightHand;

        private int lastInstanceId;
        private bool interactedLastFrame;

        // [EnumFlags]
        // public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.
        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (!interactedLastFrame && Physics.Raycast(leftHand.transform.position, leftHand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
            {
                GameObject obj = hit.collider.gameObject;
                lastInstanceId = obj.GetInstanceID();
                Debug.Log("we have hit " + lastInstanceId);
                interactedLastFrame = true;
            }
            else
            {
                lastInstanceId = -1;
                interactedLastFrame = false;
            }
            // Debug.Log(leftHand.transform.eulerAngles);
            if (debugMode)
            {
                Debug.DrawRay(leftHand.transform.position, leftHand.transform.rotation * transform.forward * 1000, Color.green);
                Debug.DrawRay(rightHand.transform.position, rightHand.transform.rotation * transform.forward * 1000, Color.blue);
            }

        }

    }

}

