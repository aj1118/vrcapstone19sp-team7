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

        private int lastInstanceIdLeft;
        private bool interactedLastFrameLeft;

        private int lastInstanceIdRight;
        private bool interactedLastFrameRight;


        public LineRenderer leftLineRenderer;
        public LineRenderer rightLineRenderer;

        // [EnumFlags]
        // public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.
        // Start is called before the first frame update


        // Update is called once per frame
        void Update()
        {
            {
                RaycastHit hit;
                if (!interactedLastFrameLeft && Physics.Raycast(leftHand.transform.position, leftHand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
                {
                    GameObject obj = hit.collider.gameObject;
                    hit.collider.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);

                    lastInstanceIdLeft = obj.GetInstanceID();
                    interactedLastFrameLeft = true;
                    Debug.Log("we have hit " + lastInstanceIdLeft);

                }
                else
                {
                    lastInstanceIdLeft = -1;
                    interactedLastFrameLeft = false;
                }
            }
            
            {
                RaycastHit hit;
                if (!interactedLastFrameRight && Physics.Raycast(rightHand.transform.position, rightHand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
                {
                    GameObject obj = hit.collider.gameObject;
                    hit.collider.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);

                    lastInstanceIdRight = obj.GetInstanceID();
                    interactedLastFrameRight = true;
                    Debug.Log("we have hit " + lastInstanceIdRight);

                }
                else
                {
                    lastInstanceIdRight = -1;
                    interactedLastFrameRight = false;
                }
            }

            if (debugMode)
            {
                Debug.DrawRay(leftHand.transform.position, leftHand.transform.rotation * transform.forward * 1000, Color.green);
                Debug.DrawRay(rightHand.transform.position, rightHand.transform.rotation * transform.forward * 1000, Color.blue);
            }

            if (leftLineRenderer != null)
            {
                Vector3 start = leftHand.transform.position;
                Vector3 end = leftHand.transform.rotation * transform.forward * 1000 + start;

                leftLineRenderer.SetPosition(0, start);
                leftLineRenderer.SetPosition(1, end);
            }

            if (rightLineRenderer != null)
            {
                Vector3 start = rightHand.transform.position;
                Vector3 end = rightHand.transform.rotation * transform.forward * 1000 + start;

                rightLineRenderer.SetPosition(0, start);
                rightLineRenderer.SetPosition(1, end);
            }

        }

        private void PerformHitRaycast(Hand hand)
        {

            // RaycastHit hit;
            // if (!interactedLastFrame && Physics.Raycast(hand.transform.position, hand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
            // {
            //     GameObject obj = hit.collider.gameObject;
            //     hit.collider.SendMessage("OnHit");

            //     lastInstanceId = obj.GetInstanceID();
            //     interactedLastFrame = true;
            //     Debug.Log("we have hit " + lastInstanceId);
            // }
            // else
            // {
            //     lastInstanceId = -1;
            //     interactedLastFrame = false;
            // }
        }

    }

}

