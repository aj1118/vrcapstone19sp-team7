using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class ThrowMechanics : MonoBehaviour
    {
        public SteamVR_Action_Boolean releaseAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        public Hand leftHand;
        public Hand rightHand;

        public float releaseVelocityTimeOffset = -0.011f;

        public float scaleReleaseVelocity = 1.1f;
        public bool throwEnabled;

        public bool MOUSE_DEBUG = false;

        private CustomSpawnAndAttachToHand spawn;
        private GameObject leftThrowable = null;
        private GameObject rightThrowable = null;
        private Renderer leftRenderer;
        private Renderer rightRenderer;

        void Start() 
        {
            spawn = GetComponent<CustomSpawnAndAttachToHand>();
            //leftRenderer = leftHand.transform.Find("LeftHandSphere").GetComponent<Renderer>();
            //rightRenderer = rightHand.transform.Find("RightHandSphere").GetComponent<Renderer>();
        }

        void FixedUpdate()
        {
            if (throwEnabled)
            {
                if (leftThrowable == null && IsButtonDown(leftHand))
                {
                    leftThrowable = spawn.SpawnAndAttach(leftHand);
                    //leftRenderer.enabled = false;
                } else
                {
                   // leftRenderer.enabled = true;
                }
                if (rightThrowable == null && IsButtonDown(rightHand))
                {
                    rightThrowable = spawn.SpawnAndAttach(rightHand);
                    //rightRenderer.enabled = false;
                } else
                {
                    //rightRenderer.enabled = true;
                }
                if (WasButtonReleased(leftHand))
                {
                    leftHand.DetachObject(leftThrowable);
                    leftThrowable = null;
                }
                if (WasButtonReleased(rightHand))
                {
                    rightHand.DetachObject(rightThrowable);
                    rightThrowable = null;
                }
            }
        }

        private bool IsButtonDown(Hand hand) {
            if (MOUSE_DEBUG)
            {
                return Input.GetKeyDown(KeyCode.T);
            }
            else
            {
                return releaseAction.GetStateDown(hand.handType);
            }
        }

        private bool WasButtonReleased(Hand hand)
        {
            if (MOUSE_DEBUG)
            {
                return Input.GetKeyUp(KeyCode.T);
            }
            else
            {
                return releaseAction.GetStateUp(hand.handType);
            }
        }

        public bool ThrowEnabled
        {
            set
            {
                throwEnabled = value;
            }
            get
            {
                return throwEnabled;
            }
        }
    }
}
