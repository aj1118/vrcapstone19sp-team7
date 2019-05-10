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
        private GameObject currentThrowObjL;
        private GameObject currentThrowObjR;

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
            waitForLoop = true;

            Debug.Log("instantiating");
            currentThrowObjL = Instantiate(throwObjPrefab, transform.position, Quaternion.identity);
            Debug.Log("spawning");
            spawning.SpawnAndAttach(leftHand, throwObjPrefab);

            Debug.Log("instantiating right");
            currentThrowObjR = Instantiate(throwObjPrefab, transform.position, Quaternion.identity);
            Debug.Log("spawning right");
            spawning.SpawnAndAttach(rightHand, currentThrowObjR);

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
                        currentThrowObjR.GetComponent<Renderer>().material = targetOn;
                        StartCoroutine(LateDetach(rightHand, currentThrowObjR));
                        notePlaying = true;
                    }
                }
                else if (notePlaying)
                {
                    notePlaying = false;
                    target.GetComponent<Renderer>().material = targetOff;

                    // Right hand
                    currentThrowObjR = Instantiate(throwObjPrefab, transform.position, Quaternion.identity);
                    spawning.SpawnAndAttach(rightHand, currentThrowObjR);
                }
            }
            if (beatIndex < beatInfo.beats.Length)
            {
                Invoke("RunTarget", beatInfo.beatTime);
            }
        }

        private IEnumerator LateDetach(Hand hand, GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();

            hand.DetachObject(gameObject, false);
        }

        public void ResetTargets()
        {
            NewLoop();
            waitForLoop = true;
        }
    }
}
