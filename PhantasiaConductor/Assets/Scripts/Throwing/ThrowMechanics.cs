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
        private GameObject leftObj;
        private GameObject rightObj;

        void Start() 
        {
            spawn = GetComponent<CustomSpawnAndAttachToHand>();
            leftObj = null;
            rightObj = null;
        }

        void FixedUpdate()
        {
            if (throwEnabled)
            {
                if (leftObj == null && IsButtonDown(leftHand))
                {
                    leftObj = spawn.SpawnAndAttach(leftHand);
                }
                if (rightObj == null && IsButtonDown(rightHand))
                {
                    rightObj = spawn.SpawnAndAttach(rightHand);
                }
                if (WasButtonReleased(leftHand))
                {
                    leftHand.DetachObject(leftObj);
                    leftObj = null;
                }
                if (WasButtonReleased(rightHand))
                {
                    rightHand.DetachObject(rightObj);
                    rightObj = null;
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
