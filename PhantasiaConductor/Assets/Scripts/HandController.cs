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

        private List<Hand> hands = new List<Hand>();


        public LineRenderer leftLineRenderer;
        public LineRenderer rightLineRenderer;

        private SteamVR_Action_Boolean gripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
        private SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

        private SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

        // [EnumFlags]
        // public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.
        // Start is called before the first frame update

        void Start()
        {
            hands.Add(rightHand);
            hands.Add(leftHand);
        }


        // Update is called once per frame
        void Update()
        {
            foreach (var hand in hands)
            {
                // Raycast only once for each hand
                GameObject obj = PerformRaycast(hand);
                if (obj == null)
                {
                    continue;
                }

                if (pinchAction.GetStateDown(hand.handType))
                {
                    obj.SendMessage("OnPinched", SendMessageOptions.DontRequireReceiver);
                }

                if (gripAction.GetStateDown(hand.handType))
                {
                    obj.SendMessage("OnGripped", SendMessageOptions.DontRequireReceiver);

                }

                obj.SendMessage("OnTracked", SendMessageOptions.DontRequireReceiver);
            }


            {
                RaycastHit hit;
                if (!interactedLastFrameLeft && Physics.Raycast(leftHand.transform.position, leftHand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
                {
                    GameObject obj = hit.collider.gameObject;
                    hit.collider.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);

                    lastInstanceIdLeft = obj.GetInstanceID();
                    interactedLastFrameLeft = true;
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

        private GameObject PerformRaycast(Hand hand)
        {
            RaycastHit hit;
            if (Physics.Raycast(hand.transform.position, hand.transform.rotation * transform.forward, out hit, Mathf.Infinity, ~(1 << 2)))
            {
                GameObject obj = hit.collider.gameObject;
                return obj;
            }
            return null;
        }
        
    }

}

