using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class ChordController : MonoBehaviour
    {
        public GameObject target;
        public GameObject throwObjPrefab;
        public Hand leftHand;
        public Hand rightHand;

        public Material targetOn;
        public Material targetOff;

        public UnityEvent onCompleteChord;

        private BeatInfo beatInfo;
        private CustomSpawnAndAttachToHand spawning;
        private int beatIndex = -1;
        private GameObject leftObj;
        private GameObject rightObj;

        private bool waitForLoop = false;
        private bool notePlaying = false;
        private bool complete = false;
        private bool invokeComplete = false;

        private void Awake()
        {
            beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();
            spawning = GetComponent<CustomSpawnAndAttachToHand>();
        }

        private void OnEnable()
        {
            waitForLoop = false;

           
            // leftObj = spawning.SpawnAndAttach(leftHand);

            // rightObj = spawning.SpawnAndAttach(rightHand);

            RunTarget();
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        public void NewLoop()
        {
            if (!complete)
            {
                beatIndex = -1;
            } else if (invokeComplete)
            {
                invokeComplete = false;
                onCompleteChord.Invoke();
            }

        }

        private void RunTarget()
        {
            if (!waitForLoop)
            {
                bool nextBeat = beatInfo.beats[(beatIndex + 1) % beatInfo.beats.Length];
                beatIndex++;

                if (nextBeat)
                {

                    if (!notePlaying)
                    {
                        target.GetComponent<Renderer>().material = targetOn;

                        // Right Hand
                        // Debug.Log("changing material");
                        // rightObj.GetComponent<Renderer>().material = targetOn;
                        // StartCoroutine(LateDetach(rightHand, rightObj));
                        notePlaying = true;
                    }
                }
                else if (notePlaying)
                {
                    notePlaying = false;
                    target.GetComponent<Renderer>().material = targetOff;

                    // Right hand
                    // rightObj = spawning.SpawnAndAttach(rightHand);
                    // Debug.Log("new right object " + rightObj);
                }
            }
            // if (beatIndex < beatInfo.beats.Length)
            // {
                Invoke("RunTarget", beatInfo.beatTime);
            // }
        }

        private IEnumerator LateDetach(Hand hand, GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();

            // Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            hand.DetachObject(gameObject, false);
            // rb.velocity = hand.GetTrackedObjectVelocity(-0.011f) * 4;
            // rb.angularVelocity = hand.GetTrackedObjectAngularVelocity(-0.011f);
            
            Debug.Log("velocity " + gameObject.GetComponent<Rigidbody>().velocity);
        }

        public void ResetTargets()
        {
            NewLoop();
            waitForLoop = true;
        }
    }
}
